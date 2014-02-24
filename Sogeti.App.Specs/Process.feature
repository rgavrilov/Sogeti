﻿Feature: Process
	To get a job
	As a developer
	I want to demonstrate my application works

Scenario: Use default filter
	Given following file sample1.csv:
	"""
	1,fname1 lname1,http://link-1,2003-12-31,2004-12-31,party1,portrait1,thumbnail1,homeState1
	2,fname2 lname2,http://link-2,2003-12-31,2004-12-31,party2,portrait2,thumbnail2,homeState2
	3,fname3 lname3,http://link-3,2003-12-31,2004-12-31,party1,portrait3,thumbnail3,New York
	4,fname4 lname4,http://link-4,2003-12-31,2004-12-31,party2,portrait4,thumbnail4,homeState4
	"""
	When I run application with /i:sample1.csv
	Then the result should match NoFilterOutput

Scenario: State filter is specified
	Given following file sample1.csv:
	"""
	1,fname1 lname1,http://link-1,2003-12-31,2004-12-31,party1,portrait1,thumbnail1,homeState1
	2,fname2 lname2,http://link-2,2003-12-31,2004-12-31,party2,portrait2,thumbnail2,homeState2
	"""
	When I run application with /i:sample1.csv /s:homeState2
	Then the result should match WithStateFilterOutput

Scenario: File is not found
	When I run application with /i:missingFile.csv
	Then the result should match FileNotFoundOutput

Scenario: Use real input file
	Given following file presidencs.csv:
	"""
	1,George Washington,http://en.wikipedia.org/wiki/George_Washington,1789-04-30,1797-03-04,Independent ,GeorgeWashington.jpg,thmb_GeorgeWashington.jpg,Virginia
	2,John Adams,http://en.wikipedia.org/wiki/John_Adams,1797-03-04,1801-03-04,Federalist ,JohnAdams.jpg,thmb_JohnAdams.jpg,Massachusetts
	3,Thomas Jefferson,http://en.wikipedia.org/wiki/Thomas_Jefferson,1801-03-04,1809-03-04,Democratic-Republican ,ThomasJefferson.jpg,thmb_Thomasjefferson.gif,Virginia
	4,James Madison,http://en.wikipedia.org/wiki/James_Madison,1809-03-04,1817-03-04,Democratic-Republican ,JamesMadison.jpg,thmb_JamesMadison.gif,Virginia
	5,James Monroe,http://en.wikipedia.org/wiki/James_Monroe,1817-03-04,1825-03-04,Democratic-Republican ,JamesMonroe.gif,thmb_JamesMonroe.gif,Virginia
	6,John Quincy Adams,http://en.wikipedia.org/wiki/John_Quincy_Adams,1825-03-04,1829-03-04,Democratic-Republican/National Republican ,JohnQuincyAdams.jpg,thmb_JohnQuincyAdams.gif,Massachusetts
	7,Andrew Jackson,http://en.wikipedia.org/wiki/Andrew_Jackson,1829-03-04,1837-03-04,Democratic ,AndrewJackson.gif,thmb_Andrew_jackson.gif,Tennessee
	8,Martin Van Buren,http://en.wikipedia.org/wiki/Martin_Van_Buren,1837-03-04,1841-03-04,Democratic,MartinVanBuren.jpg,thmb_MartinVanBuren.gif,New York
	9,William Henry Harrison,http://en.wikipedia.org/wiki/William_Henry_Harrison,1841-03-04,1841-04-04,Whig,WilliamHenryHarrison.jpg,thmb_WilliamHenryHarrison.gif,Ohio
	10,John Tyler,http://en.wikipedia.org/wiki/John_Tyler,1841-04-04,1845-03-04,Whig,JohnTyler.jpg,thmb_JohnTyler.jpg,Virginia
	11,James K. Polk,http://en.wikipedia.org/wiki/James_K._Polk,1845-03-04,1849-03-04,Democratic ,JamesKnoxPolk.jpg,thmb_JamesKPolk.gif,Tennessee
	12,Zachary Taylor,http://en.wikipedia.org/wiki/Zachary_Taylor,1849-03-04,1850-07-09,Whig,ZacharyTaylor.jpg,thmb_ZacharyTaylor.jpg,Louisiana
	13,Millard Fillmore,http://en.wikipedia.org/wiki/Millard_Fillmore,1850-07-09,1853-03-04,Whig,MillardFillmore.jpg,thmb_MillardFillmore.png,New York
	14,Franklin Pierce,http://en.wikipedia.org/wiki/Franklin_Pierce,1853-03-04,1857-03-04,Democratic ,FranklinPierce.jpg,thmb_FranklinPierce.gif,New Hampshire
	15,James Buchanan,http://en.wikipedia.org/wiki/James_Buchanan,1857-03-04,1861-03-04,Democratic ,JamesBuchanan.jpg,thmb_JamesBuchanan.gif,Pennsylvania
	16,Abraham Lincoln,http://en.wikipedia.org/wiki/Abraham_Lincoln,1861-03-04,1865-04-15,Republican/National Union,AbrahamLincoln.jpg,thmb_AbrahamLincoln.jpg,Illinois
	17,Andrew Johnson,http://en.wikipedia.org/wiki/Andrew_Johnson,1865-04-15,1869-03-04,Democratic/National Union,AndrewJohnson.jpg,thmb_AndrewJohnson.gif,Tennessee
	18,Ulysses S. Grant,http://en.wikipedia.org/wiki/Ulysses_S._Grant,1869-03-04,1877-03-04,Republican ,UlyssesSGrant.jpg,thmb_UlyssesSGrant.gif,Ohio
	19,Rutherford B. Hayes,http://en.wikipedia.org/wiki/Rutherford_B._Hayes,1877-03-04,1881-03-04,Republican ,RutherfordBHayes.jpg,thmb_RutherfordBHayes.png,Ohio
	20,James A. Garfield,http://en.wikipedia.org/wiki/James_A._Garfield,1881-03-04,1881-09-19,Republican ,James_Garfield.jpg,thmb_James_Garfield.jpg,Ohio
	21,Chester A. Arthur,http://en.wikipedia.org/wiki/Chester_A._Arthur,1881-09-19,1885-03-04,Republican ,ChesterAArthur.gif,thmb_ChesterAArthur.gif,New York
	22,Grover Cleveland,http://en.wikipedia.org/wiki/Grover_Cleveland,1885-03-04,1889-03-04,Democratic ,Grover_Cleveland_2.jpg,thmb_Grover_Cleveland_2.jpg,New York
	23,Benjamin Harrison,http://en.wikipedia.org/wiki/Benjamin_Harrison,1889-03-04,1893-03-04,Republican ,BenjaminHarrison.jpg,thmb_BenjaminHarrison.gif,Indiana
	24,Grover Cleveland (2nd term),http://en.wikipedia.org/wiki/Grover_Cleveland,1893-03-04,1897-03-04,Democratic ,Grover_Cleveland.jpg,thmb_Grover_Cleveland.jpg,New York
	25,William McKinley,http://en.wikipedia.org/wiki/William_McKinley,1897-03-04,1901-09-14,Republican ,WilliamMcKinley.jpg,thmb_WilliamMcKinley.gif,Ohio
	26,Theodore Roosevelt,http://en.wikipedia.org/wiki/Theodore_Roosevelt,1901-09-14,1909-03-04,Republican ,TheodoreRoosevelt.jpg,thmb_TheodoreRoosevelt.jpg,New York
	27,William Howard Taft,http://en.wikipedia.org/wiki/William_Howard_Taft,1909-03-04,1913-03-04,Republican ,WilliamHowardTaft.jpg,thmb_WilliamHowardTaft.jpg,Ohio
	28,Woodrow Wilson,http://en.wikipedia.org/wiki/Woodrow_Wilson,1913-03-04,1921-03-04,Democratic ,WoodrowWilson.jpg,thmb_WoodrowWilson.gif,New Jersey
	29,Warren G. Harding,http://en.wikipedia.org/wiki/Warren_G._Harding,1921-03-04,1923-08-02,Republican ,WarrenGHarding.jpg,thmb_WarrenGHarding.gif,Ohio
	30,Calvin Coolidge,http://en.wikipedia.org/wiki/Calvin_Coolidge,1923-08-02,1929-03-04,Republican ,CoolidgeWHPortrait.jpg,thmb_CoolidgeWHPortrait.gif,Massachusetts
	31,Herbert Hoover,http://en.wikipedia.org/wiki/Herbert_Hoover,1929-03-04,1933-03-04,Republican ,HerbertHover.jpg,thmb_HerbertHover.gif,Iowa
	32,Franklin D. Roosevelt,http://en.wikipedia.org/wiki/Franklin_D._Roosevelt,1933-03-04,1945-04-12,Democratic,FranklinDRoosevelt.jpg,thmb_FranklinDRoosevelt.gif,New York
	33,Harry S. Truman,http://en.wikipedia.org/wiki/Harry_S._Truman,1945-04-12,1953-01-20,Democratic,HarryTruman.jpg,thmb_HarryTruman.jpg,Missouri
	34,Dwight D. Eisenhower,http://en.wikipedia.org/wiki/Dwight_D._Eisenhower,1953-01-20,1961-01-20,Republican ,Dwight_D_Eisenhower.jpg,thmb_Dwight_D_Eisenhower.jpg,Texas
	35,John F. Kennedy,http://en.wikipedia.org/wiki/John_F._Kennedy,1961-01-20,1963-11-22,Democratic,John_F_Kennedy.jpg,thmb_John_F_Kennedy.jpg,Massachusetts
	36,Lyndon B. Johnson,http://en.wikipedia.org/wiki/Lyndon_B._Johnson,1963-11-22,1969-01-20,Democratic,Lyndon_B_Johnson.png,thmb_Lyndon_B_Johnson.gif,Texas
	37,Richard Nixon,http://en.wikipedia.org/wiki/Richard_Nixon,1969-01-20,1974-08-09,Republican,RichardNixon.gif,thmb_RichardNixon.gif,California
	38,Gerald Ford,http://en.wikipedia.org/wiki/Gerald_Ford,1974-08-09,1977-01-20,Republican,Gerald_R_Ford.jpg,thmb_Gerald_R_Ford.jpg,Michigan
	39,Jimmy Carter,http://en.wikipedia.org/wiki/Jimmy_Carter,1977-01-20,1981-01-20,Democratic ,James_E_Carter.jpg,thmb_James_E_Carter.gif,Georgia
	40,Ronald Reagan,http://en.wikipedia.org/wiki/Ronald_Reagan,1981-01-20,1989-01-20,Republican ,ReaganWH.jpg,thmb_ReaganWH.jpg,California
	41,George H. W. Bush,http://en.wikipedia.org/wiki/George_H._W._Bush,1989-01-20,1993-01-20,Republican ,George_H_W_Bush.jpg,thmb_George_H_W_Bush.gif,Texas
	42,Bill Clinton,http://en.wikipedia.org/wiki/Bill_Clinton,1993-01-20,2001-01-20,Democratic ,Clinton.jpg,thmb_Clinton.jpg,Arkansas
	43,George W. Bush,http://en.wikipedia.org/wiki/George_W._Bush,2001-01-20,2009-01-20,Republican ,George_W_Bush.jpg,thmb_George_W_Bush.jpg,Texas
	44,Barack Obama,http://en.wikipedia.org/wiki/Barack_Obama,2009-01-20,2013-01-20,  Democratic   ,Barack_Obama.jpg,thmb_Barack_Obama.jpg,Illinois
	"""
	When I run application with /i:presidencs.csv
	Then the result should match RealOutput