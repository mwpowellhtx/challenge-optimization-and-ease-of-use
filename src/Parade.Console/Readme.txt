
https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9

Parade

* Code Correctness
* Algorithmic Problem Solving
* Attention to Detail

A nearby city has recently undergone a massive revitalization effort and, in order to celebrate and
attract economic investment, is going to throw a parade.  The mayor plans to deploy a number of
security forces for the days leading up to the parade to keep the parade route free of vandalism.
However, the budget is limited, so the mayor wants to make sure the security is deployed in such a
way as to maximize effectiveness.

You are given a list of integers representing the threat of vandalism occurring on the city blocks
along the parade route--0 means vandalism will not occur on a block, and greater integers indicate
a greater danger of vandalism occurring.  The parade is planned to move in a straight line and pass
by every block exactly once.  You are also given several security forces, each of which can patrol
a number of adjacent blocks, totally nullifying the threat on the blocks they patrol.  The forces
come in different types with different patrol lengths; for example, an officer on bike can patrol
farther than an officer on foot.  The forces are represented by a list of pairs of integers, where
the first integer is the number of adjacent blocks a type of force can patrol, and the second is
how many forces are available of that type.

The number of forces available is limited so you must place them strategically to minimize the sum
of threat levels of all blocks that are not patrolled.  Because the minimum threat level may be
achieved by multiple arrangements of security, we ask that you output only the minimum total threat
level that can be achieved, and not the positions of the forces.

Input

Input should be read from standard input.  The first line consists of two space-separated integers.
The first integer represents B, the number of city blocks the parade passes through, and the second
integer represents P, the number of different patrol lengths among all available forces. The second
line contains B space-separated integers between 0 and 10^7, with the i-th integer representing the
threat level of vandalism occurring on the i-th block. The third line contains 2P space-separated
integers meant to be read two at a time, for a total of P pairs. In each pair, the first integer
represents the number of adjacent blocks that a specific type of force can patrol, and the second
integer represents the number of forces of that type that you have available.

Let F be the total number of forces across all patrol types.  Your code will be tested against the
following test case sets:

* Test case set A (code correctness metric): 0 <= B <= 100, P = 1, F = 1. Note that this means that
for this first set of test cases, you only need to position one force.

* Test case set B (advanced CS metric): 0 <= B <= 10000, P = 1, 0 <= F <= 1000

* Test case set C (advanced CS metric): 0 <= B <= 1000, 0 <= P <= 3, 0 <= F <= 100

Your score in advanced CS will increase in proportion to the size of the test cases you can handle.

Output

Write a single integer to standard output denoting the minimum total threat level that it is
possible to achieve.

Example A:

Input

7 1
0 2 5 5 4 0 6
3 1

Output

8

Explanation:

There is only one security force to deploy and it can only patrol a span of 3 adjacent blocks.  The
best place to patrol is the span of three blocks with threats of 5, 5, 4; this makes the total
remaining threat 8.  The security force can't patrol the 5, 5, 6 blocks because they are not
adjacent, and the 5, 5, 4 span is more threatening than the 4, 0, 6 span.

Example B:

Input

7 1
0 3 5 5 5 1 0
3 2

Output

0

Explanation:

There are two forces to deploy that can each patrol a span of 3 adjacent blocks.  Placing one to
patrol the 3, 5, 5 blocks and the other to patrol the 5, 1, 0 blocks reduces the total threat to 0.

Example C:

Input

10 2
4 5 0 2 5 6 4 0 3 5
3 1 2 2

Output

2

Explanation:

There is one force of patrol length 3 and two forces of patrol length 2 to deploy.  Placing the
force of length 3 to cover 5, 6, 4, and the forces of length 2 to cover 4, 5 and 3, 5, is the
optimal arrangement, leaving only a single block of threat 2 unpatrolled.

What metrics are associated with this question?

Test case set A, whose parameters are shown in the Input description, counts towards code
correctness. Your solution needs to be correct, but not necessarily clever or efficient to pass
those cases. Test case sets B and C test whether you developed a well-optimized solution and count
towards the Algorithmic Problem Solving metric.

Why this question?

This question is designed to challenge you to find an optimized solution for a computationally
difficult problem even as it becomes more difficult with the introduction of new variables.  Some
of the companies looking for strong backend developers are looking for this skill.

https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9
