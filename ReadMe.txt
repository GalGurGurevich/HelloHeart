Hi,

so basic instructions to run the project would be

1. Download and Unzip the file

2. If you don't have Dotnet installed on your computer do so here: https://dotnet.microsoft.com/download

3. if you don't have node.js installed on your computer do so here: https://nodejs.org/en/

4. Open the folder file and use any cmd, 
In folder path ClientApp type npm install
in folder HelloHeart type dotnet run
open your browser at: https://localhost:5001/

Notes:

# I have only done styling for Mobile view, the app looks bad on Desktop mode please forgive me for this. I thought it's too time-consuming and the position is mobile-oriented
# I thought to add another get function on the initial upload of the app to call get config data set,
   so I could save it to cache automatically so that the first user search will take less time, but then decided that since it's not certain the user will make a search could cause a redundant exit. 
   thus the logic remiand as is written
# I thought of the option to add error handling in the server but wanted to put more focus on the main gaols since the margin for such errors are slim and has still somehow been taken care of. 
   adding some try-catch is something I considered
 