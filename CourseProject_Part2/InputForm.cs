using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mims_CourseProject_Part2
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Hide();
        }

        private void HourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if( HourlyRadioButton.Checked )
            {
                SalaryLabel.Visible = false;
                SalaryTextbox.Visible = false;

                HourlyRateLabel.Visible = true;
                HourlyRateTextbox.Visible = true;
                HoursWorkedLabel.Visible = true;
                HoursWorkedTextbox.Visible = true;

                SalaryRadioButton.Checked = false;
            }
        }

        private void SalaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SalaryRadioButton.Checked)
            {
                SalaryLabel.Visible = true;
                SalaryTextbox.Visible = true;

                HourlyRateLabel.Visible = false;
                HourlyRateTextbox.Visible = false;
                HoursWorkedLabel.Visible = false;
                HoursWorkedTextbox.Visible = false;

                HourlyRadioButton.Checked = false;
            }
        }

    }
}
