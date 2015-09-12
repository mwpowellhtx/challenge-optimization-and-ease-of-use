
https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9

Ease of Use

* Debugging
* Attention to Detail

Note:

In this debugging challenge, the goal is to remedy the described problem while changing as little
code as possible. You're not trying to minimize the number of keystrokes (or something equally
silly), but you should only change parts that are incorrect and could be contributing to the
described problem.

Select your choice of language in the code editor to receive the initial (buggy) code. Since
you're dealing with existing code, the language choice is limited to some of the most common OO
languages.

A personal organizer app allows users to write notes about their day-to-day lives.  Each note can
have a comma-separated string of tags, where a tag is a short word or phrase intended to help
search for the note later.  For example, a cookie recipe might have tags
"todo,cookie,recipe,bake,chocolate chip".  Because tags are more useful when they are associated
with many different notes, the app has a suggestion feature for when users are typing in the
tag string that displays the tags the user might be typing.

Unfortunately, users are reporting that the suggestion feature isn't very useful, and that they're
having trouble finding tags they're certain they used before.  You have been tasked to take a look
at the implementation of the tag suggester and ensure the specification was followed.

The problems are likely caused by how typos are dealt with and how suggestions are ordered.  It is
assumed the user may make a single typo when typing the beginning of the tag (the prefix).  There
are three ways a user might be expected to make a typo:

* by typing one key when they meant to type another ("cpok" when trying to type the beginning of
"cookie")

* forgetting to type a key ("cooi" instead of "cooki")

* or typing an extra key ("cookk" instead of "cook").

Additionally, the list of suggestions should be sorted as follows:

* If the user can continue adding characters to the prefix and match a tag, that tag should appear
before a tag that can only be matched if the prefix has a typo.  So if the prefix is "ba" then
"bake" should appear before "bread."

* If two tags assume the same number of typos then a tag that occurs in more notes should appear
before a tag that occurs in fewer notes.  However, if the user mistakenly includes the same tag
more than once in a note, the tag should not be counted more than once.

* If both tags occur in the same number of notes then the tags should be ordered lexicographically
(where a appears before z).

An important consideration is when two tags are considered the same.  Leading and trailing white
space should be ignored for tags.  If two tags have the same letters but different capitalizations
they are considered equal.  However, for the purpose of displaying tags, the capitalization of the
most recently added version of the tag should be used.

Finally, if the user has not typed anything or no tags were matched then no suggestions should
appear.

The current test passes, but you should still check to make sure the previous developer didn't miss
anything.  Can you find and fix whatever bugs there may be?

Input

The input should be read from standard input.  The first line has an integer N representing the
number of notes there are.  N comma-separated lists of tags follow, one list per line, that apply
to a particular note.  For brevity the actual contents of the note are not included--only the tag
line is shown.  The next line has integer S for how many searches are done.  Finally there are S
searches, one per line, that represent the characters the user types as the user tries to type a
tag.

Output

For each of the S searches, output a comma-separated list of suggestions to offer the user as the
user types the prefix.

Test Case

Input:

5
bake,apple pie
bake,bread
bake, cherry pie
buy,apples,Gold delicious
buy,apples,Red Delicious
3
aa
baker
bu

Output:

bake,apples,apple pie
bake
buy,bake,bread

Explanation:

In the first search, bake, apples, and apple pie are suggested even though the prefix is not a
perfect match because a single letter substitution can be done to turn "aa" into "ba" or "ap".
"bake" is included before "apple" because it is found in the tag string of more notes.

The second search is an example of deleting a single letter to have the prefix match the start
of the tag.

In the third search, buy appears before bake even though bake is in more notes because "bu"
matches the start of buy perfectly, whereas the user would have made a typo when entering "bu" if
they were searching for bake.  "bread" appears after bake because bread appears in fewer notes
than bake.

What metrics are associated with this question?

This question counts towards the debugging and attention to detail metrics.

Why this question?

Because much of development work involves interacting with existing code, the ability to
understand and refactor the code of other developers is valued by many companies.

Note:

Keep in mind you will be asked in the second section what changes you made to the original code.

https://app.devdraft.com/#!/workspace/72e6d226-daa5-4333-8473-850853d62aa9
