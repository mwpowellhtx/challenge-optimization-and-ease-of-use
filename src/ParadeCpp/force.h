#pragma once

#include <common\cloneable.hpp>

namespace challenges {

    namespace parade {

        class force : public cloneable<force> {

            static int _count;

            int _id;

            int _range;

        public:

            force(int range = -1);

            force(force const & other);

            virtual ~force();

            int id() const;

            int range() const;

            bool has_range() const;

            virtual derived_type clone() const;
        };
    }
}
