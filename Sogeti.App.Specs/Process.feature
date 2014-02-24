Feature: Process
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
	Then the result should match DefaultFilterOutput

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

Scenario: Use real input file (default, text output)
	Given resource Presidents as file presidents.csv
	When I run application with /i:presidents.csv
	Then the result should match RealOutput_Text

Scenario: Use real input file (XML output)
	Given resource Presidents as file presidents.csv
	When I run application with /i:presidents.csv /f:xml
	Then the result should match RealOutput_Xml
