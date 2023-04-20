# AppleTree
A bad git clone
<br>Please don't use this for projects that matter to you!

# Why?
- because.

# Commands
- newTree - creates a new tree
- track/addApple - select file/directory to start tracking in a tree
- submit - submits changes of tracked files to local tree
- submit n/network - submits changes of tracked files to network tree (not a thing yet)
- revert - rolls back un-submitted changes made to local tree
- revert n/network - rolls back un-submitted changes made to network tree (not a thing yet)
- pwd - prints working directory
- ptd - prints tree directory

# What are Trees, Apples, and Branches?
- A Tree is a entire project
- A Apple is a file in that project
- A Branch is a portion of that project that can be merged into the Tree (not implemented)

# What can it do?
- Create trees
- Track files (apples)
- Submit changes of tracked files to tree
- Revert any changes made to a apple to version in it's tree

# What is planned? (ordered by priority. Not activly working on this...)
1. Implement remote trees
2. Implement branches
3. Track multiple versions of files

# How do I build myself?
- Download the source from the latest release (not required but recomended)
- Extract the all the files in the .zip file
- Open the AppleTree.sln in your favorite .net IDE (AppleTee was wrote in .net7 so make sure your IDE supports that)
- Click the build button (usually towards top of window)
- Done
