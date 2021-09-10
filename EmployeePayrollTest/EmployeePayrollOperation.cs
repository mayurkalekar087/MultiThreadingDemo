using System;
using System.Collections.Generic;
using System.Text;
using MultiThreadingDemo;
using System.Threading.Tasks;

namespace EmployeePayrollTest
{
    public class EmployeePayrollOperation
    {
        /// <summary>
        /// UC-1 Without Threads
        /// </summary>
        List<EmployeeDetails> employeePayrollDetailsList = new List<EmployeeDetails>();
        public void addEmployeeToPayroll(List<EmployeeDetails> employeePayrollDetailsList)
        {
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                this.addEmployeeToPayroll(employeeData);
                Console.WriteLine("Employee added: " + employeeData.EmployeeName);
            });
            Console.WriteLine(this.employeePayrollDetailsList.ToString());

        }
         public void addEmployeeToPayroll(EmployeeDetails emp)
        {
            employeePayrollDetailsList.Add(emp);
        }
        /// <summary>
        /// UC-2 With Threads
        /// </summary>
        public void addEmployeeToPayrollWithThread(List<EmployeeDetails> employeePayrollDetailsList)
        {
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                
                Task thread = new Task(() =>
                {
                          Console.WriteLine("Employee being added: "+employeeData.EmployeeName);
                    this.addEmployeeToPayroll(employeeData);
                        Console.WriteLine("Employee added: "+employeeData.EmployeeName);
                });
                thread.Start();
            });
            Console.WriteLine(this.employeePayrollDetailsList.Count);
        }

    }
}
