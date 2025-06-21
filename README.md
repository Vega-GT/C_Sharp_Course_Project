# C_Sharp_Course_Project
A C# school project for one of my CEIS classes.

This project uses C# to create a payroll system.  It has an Employee Input Form which allows to put in different data for Employees like First Name, Last Name, Social Security Number, Hire Date, Health Insurance, Life Insurance, Vacation Days, and Salary data.  The Main Form (named Payroll System) has options for adding employees, removing employees, displaying employees, and printing paychecks.  This used the FileStream to write employees to a file, and load employees from the file.  The Print Paychecks button loops through the listed employees and displays a message box with the employee first name, last name, and the calculated pay to simulate sending to a printer.  

This project also uses custom classes such as the Employee class, the Benefits class, and the Hourly or Salary class which inherits from the Employee class.  
