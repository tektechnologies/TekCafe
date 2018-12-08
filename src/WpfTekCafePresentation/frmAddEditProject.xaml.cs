using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicLayer;
using DataObjects;

namespace WpfTekCafePresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditProject.xaml
    /// </summary>
    public partial class frmAddEditProject : Window
    {
        private ProjectManager _projecttManager = new ProjectManager();
        private Project _oldProject; // for edit and detail
        private Project _newProject; // for edit and add
        private DetailPurpose _purpose; // for detail

        public frmAddEditProject()
        {
            InitializeComponent();
            setEditable();

            this.Title = "Add a Project";
            this.btnProjectAction.Content = "Add";
            this.chkActive.IsChecked = true;
            this.chkActive.IsEnabled = false;
        }

        




        public frmAddEditProject(Project oldProject) // constructor for edit project
        {
            InitializeComponent(); // leave as first line

            _oldProject = oldProject;
            setOldProject();
            setEditable();
            this.Title = "Edit the Record for the " + _oldProject.Name;
            this.txtProjectID.IsReadOnly = true;
        }
        public frmAddEditProject(Project oldProject, DetailPurpose purpose)
        {   // constructor for detail view
            InitializeComponent();

            _oldProject = oldProject;
            setOldProject();
            setReadOnly();
        }
        private void setOldProject()
        {
            txtProjectID.Text = _oldProject.ProjectID;
            txtProjectName.Text = _oldProject.Name;
            txtDescription.Text = _oldProject.Description;
            datePurchase.DisplayDate = _oldProject.PurchaseDate;
            lblPurchaseDate.Content = "Purchase Date: " + datePurchase.DisplayDate.ToShortDateString();
            txtWorkStation.Text = _oldProject.WorkStation.ToString();
            cboProjectType.SelectedItem = _oldProject.ProjectTypeID;
            cboProjectPhase.SelectedItem = _oldProject.PhaseID;
            txtClientID.Text = _oldProject.ClientID;
            chkActive.IsChecked = _oldProject.Active;
        }

        private void setEditable()
        {
            txtProjectID.IsReadOnly = !true;
            txtProjectName.IsReadOnly = !true;
            txtDescription.IsReadOnly = !true;
            txtWorkStation.IsReadOnly = !true;
            cboProjectType.IsEnabled = !false;
            cboProjectPhase.IsEnabled = !false;
            txtClientID.IsReadOnly = !true;

            btnProjectAction.Content = "Save";
        }

        private void setReadOnly()
        {
            txtProjectID.IsReadOnly = true;
            txtProjectName.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            txtWorkStation.IsReadOnly = true;
            cboProjectType.IsEnabled = false;
            cboProjectPhase.IsEnabled = false;
            txtClientID.IsReadOnly = true;
           

            switch (_purpose)
            {
                case DetailPurpose.SellTek:
                    this.Title = "Get going with the " + _oldProject.ProjectTypeID +
                        " " + _oldProject.Name + "!";
                    break;
                case DetailPurpose.CheckOut:
                    break;
                case DetailPurpose.Reviews:
                    break;
                case DetailPurpose.HostSite:
                    break;
                case DetailPurpose.Develops:
                    break;
                case DetailPurpose.Developer:
                    break;
                default:
                    break;
            }
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboProjectType.ItemsSource = _ProjectManager.GetProjectTypes();
                cboProjectPhase.ItemsSource = _ProjectManager.GetAllProjectPhase();
            }
            catch (Exception)
            {
                MessageBox.Show("Project Types not found.");
            }
        }


        private void captureNewProject()
        {
            _newProject = new Project()
            {
                ProjectID = txtProjectID.Text,
                Name = txtProjectName.Text,
                Description = txtDescription.Text,
                PurchaseDate = datePurchase.DisplayDate,
                WorkStation = int.Parse(txtWorkStation.Text),     
                ProjectTypeID = (string)cboProjectType.SelectedValue,                
                PhaseID = (string)cboProjectPhase.SelectedValue,
                ClientID = txtClientID.Text,              
                Active = (bool)chkActive.IsChecked
            };
        }

       
           


                   



        private void btnProjectAction_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnProjectAction.Content.ToString() == "Save")
            {
                captureNewProject();

                try
                {
                    _projectManager.EditProject(_oldProject, _newProject);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update Incomplete!");
                }
            }
            else if (this.btnProjectAction.Content.ToString() == "Add")
            {
                captureNewProject();
                try
                {
                    _projectManager.AddProject(_newProject);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Add Project Incomplete!");
                }
            }
        }

        private void datePurchase_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lblPurchaseDate.Content = "Purchase Date: " + datePurchase.DisplayDate.ToShortDateString();
        }




















    }
}
