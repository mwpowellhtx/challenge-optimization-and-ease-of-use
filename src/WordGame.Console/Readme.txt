
https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9

Word Game

* Code Correctness
* Attention to Detail

We will be simulating a single-player word game.  The player is an alchemist who takes elemental
crystals and forms compounds.  Each crystal is a specific type of element, labeled F, I, Z, B, A,
or R.  The crystals are presented to the player in a line on the alchemist's workbench.  Basic
compounds form when adjacent crystals are in specific orders--namely, FIZ, BAR, BAZ, ZIF, RAB, or
ZAB (the last three are the first three in reverse).  Additionally, if adjacent crystals form a
sequence, such as BAZIF, that consists of overlapping basic compounds (BAZ and ZIF in this
example), then that sequence is treated as a single, larger compound.

When a compound forms, the crystals forming the compound are removed from the workbench and any
crystals to their right slide to the left so that there are no gaps between crystals.  After the
crystals have finished sliding it is necessary to do another check to see if any compounds formed.
You can assume that no compounds exist on the workbench at the start of the game.

The player can only rearrange crystals by swapping adjacent crystals with each other, and can only
swap if the move results in a compound forming.  If the player tries to make an illegal move, no
crystals are actually swapped.  The gameplay alternates between compounds being removed, crystals
sliding to the left to fill gaps, and the player making swaps.

Input

Input should be read from standard input.  The first line contains the integer C (0 < C < 100),
which is the number of crystals on the workbench.  The next line contains a string of the C capital
letters, with the i-th letter representing the i-th crystal on the workbench.  The third line has
the integer S (0 <= S < 50), the number of swaps the player makes.  The fourth line has the S
space-separated integers representing the index of the left letter that is to be swapped with its
neighbor to the right.  The leftmost crystal on the workbench is at index 0.

Output

Output, in order from left to right, the letters of the crystals remaining on the workbench after
the swaps are done and any compounds are removed.  Output should be written to standard output.

Example from image

Input:

10
BFIBZRAFBF
2
3 4

Output

BBFF

Explanation

The player is presented with BFIBZRAFBF and chooses to swap the letter at index 3 with its right
neighbor.  The result is:
BFIZBRAFBF

FIZ is formed, so it is removed:
B   BRAFBF

The letters slide to the left:
BBRAFBF

Now, the player chooses to swap the letter at index 4 with its right neighbor:
BBRABFF

RAB is formed, which is BAR in reverse, so it is removed:
BB   FF
BBFF

Now that all the moves have been done, the simulation ends. Final output:

BBFF

What metrics are associated with this question?

This question counts towards the code correctness and attention to detail metrics.

Why this question?

This problem is a simple problem that demonstrates your ability to analyze a situation and write correct code to address it, which is the first competency companies usually look for.

https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9
