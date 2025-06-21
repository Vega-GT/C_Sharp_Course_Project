using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mims_CourseProject_Part2
{
    public partial class MainForm : Form
    {
        // Class Level References
        private const string FILENAME = "Employee.dat";
        public MainForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //Anthony Mims 11/30/2024
            InputForm frmInput = new InputForm();

            using (frmInput)
            {
                DialogResult result = frmInput.ShowDialog();

                if(result==DialogResult.Cancel)
                {
                    return;
                }

                string fName = frmInput.FirstNameTextBox.Text;
                string lName = frmInput.LastNameTextBox.Text;
                string ssn = frmInput.SSNTextBox.Text;
                string date = frmInput.HireDateTextBox.Text;
                string healthIns = frmInput.HealthInsuranceTextBox.Text;
                int lifeIns = int.Parse(frmInput.LifeInsuranceTextBox.Text);
                int vacation = int.Parse(frmInput.VacationDaysTextBox.Text);

                Benefits ben = new Benefits(healthIns,lifeIns,vacation);

                DateTime hireDate;

                if (!DateTime.TryParse(date, out hireDate)) 
                {
                    //DateTime hireDate = DateTime.Parse(date);
                    //Check of Date Entered is correct and give error message of not.
                    //Anthony Mims 11/30/2024
                    MessageBox.Show("Date Entered Is Not Valid!");
                    return;
                }

                Employee emp = null; // empty refrence

                if ( frmInput.SalaryRadioButton.Checked)
                {
                    double salary = double.Parse(frmInput.SalaryTextbox.Text);
                    emp = new Salary(fName, lName, ssn, hireDate, ben, salary);
                }
                else if ( frmInput.HourlyRadioButton.Checked)
                {
                    double hourlyRate = double.Parse(frmInput.HourlyRateTextbox.Text);
                    double hoursWorked = double.Parse(frmInput .HoursWorkedTextbox.Text);

                    emp = new Hourly(fName, lName, ssn, hireDate, ben, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error. Please Select An Employee Type!");
                    return;
                }

                EmployeesListBox.Items.Add(emp);

                WriteEmpsToFile();
            }
        }

        private void WriteEmpsToFile()
        {
            // Convert ListBox items to generic list
            List<Employee> empList = new List<Employee>();

            foreach (Employee emp in EmployeesListBox.Items)
            {
                empList.Add(emp);
            }

            //open a pipe to file and create the translator
            FileStream fs = new FileStream(FILENAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fs, empList);

            fs.Close();
        }

        private void ReadEmpsFromFile()
        {

            if (File.Exists(FILENAME) && new FileInfo(FILENAME).Length > 0) //If this file doesn't exist, the program will crash if trying to run this code.
            {
                FileStream fs = new FileStream(FILENAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                //Read List
                List<Employee> list = (List<Employee>)formatter.Deserialize(fs);


                fs.Close();


                EmployeesListBox.Items.Clear();

                foreach (Employee emp in list)
                    EmployeesListBox.Items.Add(emp);
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int itemIndex = EmployeesListBox.SelectedIndex;

            if(itemIndex > -1)
            {
                EmployeesListBox.Items.RemoveAt(itemIndex);
                WriteEmpsToFile();
            }
            else
            {
                MessageBox.Show("Select an employee to remove!");
            }
        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            EmployeesListBox.Items.Clear();
            ReadEmpsFromFile();
        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            foreach (Employee emp in EmployeesListBox.Items)
            {
                string line1 = "Pay To: " + emp.FirstName + " " + emp.LastName;
                string line2 = "Amount Of: " + emp.CalculatePay().ToString("C2");

                string output = "Paycheck:\n\n" + line1 + "\n" + line2;
                MessageBox.Show(output);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadEmpsFromFile();
        }

        private void EmployeesListBox_DoubleClick(object sender, EventArgs e)
        {
            Employee emp = EmployeesListBox.SelectedItem as Employee;

            InputForm updateFrm = new InputForm();
            updateFrm.FirstNameTextBox.Text = emp.FirstName;
            updateFrm.LastNameTextBox.Text = emp.LastName;
            updateFrm.SSNTextBox.Text = emp.SSN;
            updateFrm.HireDateTextBox.Text = emp.HireDate.ToShortDateString();
            updateFrm.HealthInsuranceTextBox.Text = emp.BenefitsEmp.HealthInsurance;
            updateFrm.LifeInsuranceTextBox.Text = emp.BenefitsEmp.LifeInsurance.ToString();
            updateFrm.VacationDaysTextBox.Text = emp.BenefitsEmp.Vacation.ToString();

            //check Employee Type
            if( emp is Salary)
            {
                updateFrm.HourlyRateLabel.Visible = false;
                updateFrm.HourlyRateTextbox.Visible = false;
                updateFrm.HoursWorkedLabel.Visible = false;
                updateFrm.HoursWorkedTextbox.Visible = false;
                updateFrm.SalaryLabel.Visible = true;
                updateFrm.SalaryTextbox.Visible = true;

                updateFrm.SalaryRadioButton.Checked = true;
                //updateFrm.HourlyRadioButton.Checked = false;

                Salary sal = emp as Salary;

                updateFrm.SalaryTextbox.Text = sal.AnnualSalary.ToString("F2");

            }
            else if ( emp is Hourly)
            {
                updateFrm.HourlyRateLabel.Visible = true;
                updateFrm.HourlyRateTextbox.Visible = true;
                updateFrm.HoursWorkedLabel.Visible = true;
                updateFrm.HoursWorkedTextbox.Visible = true;
                updateFrm.SalaryLabel.Visible = false;
                updateFrm.SalaryTextbox.Visible = false;

                //updateFrm.SalaryRadioButton.Checked = false;
                updateFrm.HourlyRadioButton.Checked = true;

                Hourly hrly = emp as Hourly;

                updateFrm.HourlyRateTextbox.Text = hrly.HourlyRate.ToString("F2");
                updateFrm.HoursWorkedTextbox.Text = hrly.HoursWorked.ToString("F2");
            }
            else
            {
                MessageBox.Show("Error.  Invalid employee type found.");
                return;
            }

            DialogResult = updateFrm.ShowDialog();

            if (DialogResult == DialogResult.Cancel)
                return;

            // delete old selection
            int position = EmployeesListBox.SelectedIndex;
            EmployeesListBox.Items.RemoveAt(position);

            Employee newEmp = null;

            string FirstName = updateFrm.FirstNameTextBox.Text;
            string LastName = updateFrm.LastNameTextBox.Text;
            string SSN = updateFrm.SSNTextBox.Text;
            DateTime HireDate =  DateTime.Parse(updateFrm.HireDateTextBox.Text);
            string HealthInsurance = updateFrm.HealthInsuranceTextBox.Text;
            int LifeInsurance = int.Parse(updateFrm.LifeInsuranceTextBox.Text);
            int Vacation = int.Parse(updateFrm.VacationDaysTextBox.Text);

            Benefits ben = new Benefits(HealthInsurance, LifeInsurance, Vacation);

            if ( updateFrm.SalaryRadioButton.Checked )
            {
                double salary = double.Parse(updateFrm.SalaryTextbox.Text);
                newEmp = new Salary(FirstName, LastName, SSN, HireDate, ben, salary);
            }
            else if(updateFrm.HourlyRadioButton.Checked )
            {
                double hourlyRate = double.Parse(updateFrm.HourlyRateTextbox.Text);
                double hoursWorked = double.Parse(updateFrm.HoursWorkedTextbox.Text);

                newEmp = new Hourly(FirstName, LastName, SSN, HireDate, ben, hourlyRate, hoursWorked);
            }
            else
            {
                MessageBox.Show("Error. Invalid employee type.");
                return;
            }


            // add new employee 
            EmployeesListBox.Items.Add(newEmp);
            
        }
    }
}
