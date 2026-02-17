using BLL_BankManagement;
using System;
using System.Windows.Forms;
using System.IO;

namespace BankManagement.PersonalInformation.Components
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void LoadDefaultInfo()
        {
            llEditPersonInfo.Visible = false;
            lblName.Text = "[?????]";
            lblNationalNO.Text = "[?????]";
            lblGender.Text = "[?????]";
            lblEmail.Text = "[?????]";
            lblAddress.Text = "[?????]";
            lblDateOfBirth.Text = "[?????]";
            lblPhone.Text = "[?????]";
            lblCountry.Text = "[?????]";
            picboxPersonImage.Image = Properties.Resources.Male_512;
        }

        // to call it form outside when needed
        public void ResetPersonInfo()
        {
            LoadDefaultInfo();
        }

        private void LoadPersonImage()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                string ImagePath = _Person.ImagePath;

                if (File.Exists(ImagePath))
                {
                    picboxPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could not find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                picboxPersonImage.Image = _Person.Gender == 0 ? Properties.Resources.Male_512 : Properties.Resources.Female_512;
            }
            picboxGender.Image = _Person.Gender == 0 ? Properties.Resources.Man_32 : Properties.Resources.Woman_32;
        }

        public void LoadPersonInformation(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            LoadDefaultInfo();
            if (_Person == null)
            {
                MessageBox.Show($"Person with ID={PersonID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadPersonInfo();
        }

        public void LoadPersonInformation(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            LoadDefaultInfo();
            if (_Person == null)
            {
                MessageBox.Show($"Person with NationalNo={NationalNo} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadPersonInfo();
        }

        private void _LoadPersonInfo()
        {
            llEditPersonInfo.Visible = true;
            _PersonID = _Person.PersonID;
            lblName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
            lblNationalNO.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            LoadPersonImage();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID == -1)
            {
                MessageBox.Show("Please select a person to edit.", "No Person Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form form = new frmAddUpdatePerson(_PersonID);
            form.ShowDialog();

            //Refresh the person information after editing
            LoadPersonInformation(_PersonID);
        }
    }
}
