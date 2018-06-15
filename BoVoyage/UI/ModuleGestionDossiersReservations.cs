using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Framework.UI;
using BoVoyage.Metiers;

namespace BoVoyage.UI
{
    public class ModuleGestionDossiersReservations
    {
        private static readonly List<InformationAffichage> strategieAffichageGestionDossiersReservations =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<DossierReservation>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<DossierReservation>(x=>x.IdVoyage, "IdVoyage", 3),
                InformationAffichage.Creer<DossierReservation>(x=>x.NumeroUnique, "NumerUnique", 3),
                InformationAffichage.Creer<DossierReservation>(x=>x.NumeroCarteBancaire, "NumeroCarteBancaire", 50),
                InformationAffichage.Creer<DossierReservation>(x=>x.PrixTotal, "PrixTotal", 20),
                InformationAffichage.Creer<DossierReservation>(x=>x.Id, "IdClient", 10),
                InformationAffichage.Creer<DossierReservation>(x=>x.Id, "IdParticipant", 10),
            };

        private Menu menu;
        private void InitialiserMenu ()
        {
            this.menu = new Menu("Gestion des dossiers réservation");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les réservations")
            {
                FonctionAExecuter = this.AfficherReservation
            });
            this.menu.AjouterElement(new ElementMenu("2", "Créer une réservation")
            {
                FonctionAExecuter = this.CreerReservation
            });
            this.menu.AjouterElement(new ElementMenu("3", "Modifier une réservation")
            {
                FonctionAExecuter = this.ModifierReservation
            });
            this.menu.AjouterElement(new ElementMenu("4", "Supprimer une réservation")
            {
                FonctionAExecuter = this.SupprimerReservation
            });
            this.menu.AjouterElement(new ElementMenu("5", "Rechercher une réservation")
            {
                FonctionAExecuter = this.RechercherReservation
            });
            this.menu.AjouterElement(new ElementMenuQuitterMenu("R", "Revenir au menu principal"));
        }

        public void Demarrer()
        {
            if (this.menu == null)
            {
                this.InitialiserMenu();
            }

            this.menu.Afficher();
        }

        public void AfficherReservation()
        {
            ConsoleHelper.AfficherEntete("Dossier de réservation");

            var liste = Application.GetBaseDonnees().DossiersReservations.ToList();
            ConsoleHelper.AfficherListe(liste, strategieAffichageGestionDossiersReservations);
        }

        public void CreerReservation()
        {

        }

        public void ModifierReservation()
        {

        }

        public void SupprimerReservation()
        {

        }

        public void RechercherReservation()
        {

        }
    }
}
