rem use ILMerge to create single sogeti.exe file.
ilmerge /targetplatform:v4 /target:exe /out:sogeti.exe Sogeti.App.Console.exe NConsoler.dll Newtonsoft.Json.dll Ninject.dll Sogeti.App.dll Sogeti.Common.dll
