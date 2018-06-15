using BoVoyage.Framework.UI;
using BoVoyage.UI;
using System;
using System.Configuration;
using System.Data.SqlClient;
using BoVoyage.Dal;

namespace BoVoyage.UI
{
    public class Application
    {
        private Menu menuPrincipal;
        private ModuleGestionClients moduleGestionClients;
        private ModuleGestionVoyages moduleGestionVoyages; 
        private ModuleGestionDossiersReservations moduleGestionDossiersReservations;

        private ModuleGestionClients ModuleGestionClients
        {
            get => this.moduleGestionClients;
        }

        public ModuleGestionVoyages ModuleGestionVoyages
        {
            get => this.moduleGestionVoyages;
        }

        public ModuleGestionDossiersReservations ModuleGestionDossiersReservations
        {
            get => this.moduleGestionDossiersReservations;
        }

        private void InitialiserModules()
        {
            this.moduleGestionClients = new ModuleGestionClients();
            this.moduleGestionVoyages = new ModuleGestionVoyages();
            this.moduleGestionDossiersReservations = new ModuleGestionDossiersReservations();
        }

        private void InitialiserMenuPrincipal()
        {
            this.menuPrincipal = new Menu("Menu Principal");
            this.menuPrincipal.AjouterElement(new ElementMenu("1", "Gestion des dossiers de reservation")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionDossiersReservations.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("2", "Gestion des voyages")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionVoyages.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenu("3", "Gestion des clients")
            {
                AfficherLigneRetourMenuApresExecution = false,
                FonctionAExecuter = this.moduleGestionClients.Demarrer
            });
            this.menuPrincipal.AjouterElement(new ElementMenuQuitterMenu("Q", "Quitter")
            {
                FonctionAExecuter = () => Environment.Exit(1)
            });
        }

        public void Demarrer()
        {
            this.InitialiserModules();
            this.InitialiserMenuPrincipal();

            this.menuPrincipal.Afficher();
        }

        public static BaseDonnees GetBaseDonnees()
        {
            return new BaseDonnees();
        }

        public static SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString;
            return new SqlConnection(connectionString);
        }       
    }
}
