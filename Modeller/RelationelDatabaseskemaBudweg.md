CALIPER(CaliperID, ItemNumber, CaliperType)

EMPLOYEE(EmployeeID)

STARTCONTROL(StartControlID, Date, CaliperID FK→CALIPER.CaliperID, EmployeeID FK→EMPLOYEE.EmployeeID)

FINALCONTROL(FinalControlID, Date, Result, Comment, Waste, Export, CaliperID FK→CALIPER.CaliperID, EmployeeID FK→EMPLOYEE.EmployeeID)
