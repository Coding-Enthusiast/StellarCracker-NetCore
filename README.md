# StellarCracker-NetCore
This is a small console app created by request of [@LoyceV on bitcointalk](https://bitcointalk.org/index.php?topic=4959742.msg45625811#msg45625811).  
It is designed to brute force Stellar private keys that contain typos. It requires all 56 characters of the private key plus the corresponding public key
to check each permutation against.  
Current version is single threaded and will automatically start with assuming 1 char is wrong, then 2, 3, and will continue in an endless loop (technically 55 chars is max).
It will get increasingly difficult to find the correct combination as the number of wrong characters increase.  

This project is completely open source and under MIT license. It is written in c# targetting .Net 5.  
There is a single dependency for ECC called [dotnet-stellar-sdk
](https://github.com/elucidsoft/dotnet-stellar-sdk) released under Apache License 2.0

# How To Use
You can follow same steps explaind [here](https://github.com/Coding-Enthusiast/FinderOuter#getting-started) to build and run this tool. 
You can also download binaries provided in [releases](https://github.com/Coding-Enthusiast/StellarCracker-NetCore/releases) and run using  
`./StellarCracker NetCore.dll`

## Donation
If You found this tool helpful consider making a _bitcoin_ donation:  
Legacy address: 1Q9swRQuwhTtjZZ2yguFWk7m7pszknkWyk  
SegWit address: bc1q3n5t9gv40ayq68nwf0yth49dt5c799wpld376s