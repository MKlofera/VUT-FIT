cmake_minimum_required(VERSION 3.16)
project(2uloha C)

set(CMAKE_C_STANDARD 11)

include_directories(btree)
include_directories(btree/iter)
include_directories(hashtable)

add_executable(2uloha
        #        btree/iter/btree.c
        #        btree/iter/stack.c
        #        btree/iter/stack.h
        btree/rec/btree.c
        btree/btree.c
        btree/btree.h
        btree/test.c
        btree/test_util.c
        btree/test_util.h
        #        hashtable/hashtable.c
        #        hashtable/hashtable.h
        #        hashtable/test.c
        #        hashtable/test_util.c
        #        hashtable/test_util.h
        )
