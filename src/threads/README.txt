I needed to edit these files in pintos/src/tests/threads:

alarm-wait.c:
	Copied the test_alarm_multiple method as 
	test_alarm_mega and changed the arguments to 
	iterate 70 times instead of 7
	
tests.c:
	Added a line so that I could run the test_alarm_mega
	method from the command line as 'alarm-mega'
	
tests.h:
	Added the test_alarm_mega method created in alarm-wait.c