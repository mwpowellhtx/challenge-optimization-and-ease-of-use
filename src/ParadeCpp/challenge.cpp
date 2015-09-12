#include "challenge.h"

#include <common\strutil.hpp>
#include <common\parsers.hpp>

#include "combinatorial.hpp"

#include <algorithm>
#include <numeric>
#include <climits>

namespace challenges {

    namespace parade {

        challenge::challenge(std::istream * pis, std::ostream * pos)
            : challenge_base(pos),
            _blocks(),
            _forces(),
            _min_level(INT_MAX) {

            init(*pis);
        }

        challenge::~challenge() {
            finish();
        }

        std::vector<std::string> challenge::splitstr(std::string const & s) {
            static split splitter(split::remove_empty_entries);
            return splitter(s, " ");
        }

        std::vector<block> challenge::read_blocks(int count, std::vector<int> const & levels) {
            std::vector<block> result;
            for (auto const & level : levels) {
                result.push_back(block(level));
            }
            return result;
        }

        std::vector<force> challenge::read_forces(int count, std::vector<int> const & values) {
            std::vector<force> result;
            for (std::vector<int>::size_type i = 0; i < values.size(); i += 2) {
                auto range = values.at(i);
                auto force_count = values.at(i + 1);
                while (force_count--) {
                    result.push_back(force(range));
                }
            }
            return result;
        }

        std::vector<std::string> challenge::read_lines(std::istream & is) {
            auto lines = challenge_base::read_lines(is);

            std::vector<int> counts;

            for (auto const & c : splitstr(lines[0]))
                counts.push_back(to_int(c));

            std::vector<int> levels;

            for (auto const & c : splitstr(lines[1]))
                levels.push_back(to_int(c));

            _blocks = read_blocks(counts.at(0), levels);

            std::vector<int> values;

            for (auto const & c : splitstr(lines[2]))
                values.push_back(to_int(c));

            _forces = read_forces(counts.at(1), values);

            /* This bit is key: fill in the gaps with "dummy" forces that allow the different
            permutations to take hold across the range of potential coverages. */

            auto total_range = std::accumulate(_forces.begin(), _forces.end(),
                static_cast<int>(0),
                [](int const & g, force const & x) { return g + x.range(); });

            if (total_range < static_cast<int>(_blocks.size())) {

                auto max_range = _blocks.size() - total_range;

                static const int two = 2;
                static const int one = 1;

                while (max_range > one) {
                    _forces.push_back(force(-two));
                    max_range -= two;
                }

                if (max_range == one)
                    _forces.push_back(force());
            }

            return lines;
        }

        int sum_block_patrolled_levels(std::vector<block> const & blocks) {
            auto result = std::accumulate(begin(blocks), end(blocks), static_cast<int>(0),
                [](int const & g, block const & x) { return g + x.patrolled_level(); });
            return result;
        }

        bool try_patrol(force & force, std::vector<block> & blocks) {

            auto end = blocks.end();

            const auto is_patrolled = [](block const & x) { return !x.is_patrolled(); };

            auto it = std::find_if(blocks.begin(), end, is_patrolled);

            auto range = force.range();

            if (it == end || end - it < range) return false;

            const auto set_patrol = [&force](block & x) { x.patrol(&force); };

            std::for_each(it, it + range, set_patrol);

            return true;
        }

        int challenge::calculate_level() {

            auto current_level = sum_block_patrolled_levels(_blocks);

            auto begin = _forces.begin();
            auto end = _forces.end();

            const long forces_count = static_cast<long>(_forces.size());

            const int permutation_count = calculate_permutation_count(forces_count, forces_count);

            auto count = static_cast<int>(0);
            auto power = 0;
            while (std::pow(10, ++power) < permutation_count) {}
            const auto min_count = std::pow(10, std::max(3, power - 1)) / 2;

            const auto clear_patrol = [](block & x) { x.patrol(nullptr); };
            const auto permutation_pred = [](force const & a, force const & b) { return a.id() < b.id(); };

            do {

                std::for_each(_blocks.begin(), _blocks.end(), clear_patrol);

                for (auto it = begin; it != end; it++)
                    if (!try_patrol(*it, _blocks)) break;

                auto next_level = sum_block_patrolled_levels(_blocks);

                current_level = std::min(current_level, next_level);

                // Anticipate long running permutations.
                if (++count > min_count) break;

            } while (current_level && std::next_permutation(begin, end, permutation_pred));

            return current_level;
        }

        void challenge::run() {
            _min_level = calculate_level();
        }

        void challenge::report(std::ostream & os) {
            os << _min_level << std::endl;
        }
    }
}
