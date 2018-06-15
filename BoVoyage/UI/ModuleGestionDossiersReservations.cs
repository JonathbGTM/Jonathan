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
            ConsoleHelper.AfficherEntete("Nouvelle réservation");

            var reservation = new DossierReservation { };
            {
                reservation.IdVoyage = ConsoleSaisie.SaisirEntierObligatoire("IdVoyage :");
                reservation.IdClient = ConsoleSaisie.SaisirEntierObligatoire("IdClient :");
                reservation.IdParticipant = ConsoleSaisie.SaisirEntierObligatoire("IdParticipant :");
                reservation.NumeroUnique = int.Parse(ConsoleSaisie.SaisirChaineObligatoire("Entrez le numéro client : "));
                reservation.PrixTotal = int.Parse(ConsoleSaisie.SaisirChaineObligatoire("Entrez le prix Total : "));
                reservation.NumeroCarteBancaire = ConsoleSaisie.SaisirEntierObligatoire("Entrez le numéro de la carte bancaire du client : ");          

            };

            using (var bd = Application.GetBaseDonnees())
            {
                bd.DossiersReservations.Add(reservation);
                bd.SaveChanges();
            }
        }

        public void ModifierReservation()
        {

        }

        public void SupprimerReservation()
        {
            ConsoleHelper.AfficherEntete("Supprimer une réservation");
            var liste = Application.GetBaseDonnees().Clients.ToList();

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero id de la réservation : ");

            using (var sup = Application.GetBaseDonnees())
            {
                var reservation = sup.DossiersReservations.SingleOrDefault(x => x.Id == id);

                if (liste.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Aucune réservation pour le moment");
                    return;
                }

                else
                {

                    sup.DossiersReservations.Remove(reservation);
                    sup.SaveChanges();
                }
            }
        }

        public void RechercherReservation()
        {
            ConsoleHelper.AfficherEntete("Rechercher une réservation");

            var client = ConsoleSaisie.SaisirChaineObligatoire("Nom du client (réservation) recherché : ");

            using (var recherche = Application.GetBaseDonnees())

            {
                var liste = recherche.Clients.Where(x => x.Nom.Contains(client));
                ConsoleHelper.AfficherListe(liste, strategieAffichageGestionDossiersReservations);
            }
        }
    }
}
