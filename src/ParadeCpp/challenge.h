#pragma once

#include <common\challenge_base.hpp>
#include <common\strutil.hpp>

#include "block.h"
#include "force.h"

namespace challenges {

    namespace parade {

        class challenge : public challenge_base {

            std::vector<block> _blocks;

            std::vector<force> _forces;

            int _min_level;

            std::vector<std::string> splitstr(std::string const & s);

        public:

            challenge(std::istream * pis, std::ostream * pos);

            virtual ~challenge();

        protected:

            virtual std::vector<std::string> read_lines(std::istream & is);

            virtual void run();

            virtual void report(std::ostream & os);

        private:

            int calculate_level();

            std::vector<block> read_blocks(int count, std::vector<int> const & levels);

            std::vector<force> read_forces(int count, std::vector<int> const & values);
        };
    }
}
