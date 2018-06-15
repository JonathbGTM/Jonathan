using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Dal;
using BoVoyage.Framework.UI;
using BoVoyage.Metiers;

namespace BoVoyage.UI
{
    public class ModuleGestionVoyages
    {
        private static readonly List<InformationAffichage> strategieAffichageGestionVoyages =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Voyage>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Voyage>(x=>x.DateAller, "DateAller", 10),
                InformationAffichage.Creer<Voyage>(x=>x.DateRetour, "DateRetour", 10),
                InformationAffichage.Creer<Voyage>(x=>x.PlacesDisponibles, "PlaceDisponibles", 5),
                InformationAffichage.Creer<Voyage>(x=>x.TarifToutCompris, "TarifToutCompris", 5),
                InformationAffichage.Creer<Voyage>(x=>x.IdAgence, "IdAgenceVoyage", 3),
                InformationAffichage.Creer<Voyage>(x=>x.IdDestination, "IdDestination", 3),
            };

        private Menu menu;
        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des voyages");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les voyages")
            {
                FonctionAExecuter = this.AfficherVoyages
            });
            this.menu.AjouterElement(new ElementMenu("2", "Créer un voyage")
            {
                FonctionAExecuter = this.AjouterVoyage
            });
            this.menu.AjouterElement(new ElementMenu("3", "Modifier un voyage")
            {
                FonctionAExecuter = this.ModifierVoyage
            });
            this.menu.AjouterElement(new ElementMenu("4", "Supprimer un voyage")
            {
                FonctionAExecuter = this.SupprimerVoyage
            });
            this.menu.AjouterElement(new ElementMenu("5", "Rechercher un voyage")
            {
                FonctionAExecuter = this.RechercherVoyage
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

        private void AfficherVoyages()
        {
            ConsoleHelper.AfficherEntete("Voyages");

            var liste = Application.GetBaseDonnees().Voyages.ToList();
            ConsoleHelper.AfficherListe(liste, strategieAffichageGestionVoyages);
        }

        private void AjouterVoyage()
        {
            ConsoleHelper.AfficherEntete("Nouveau voyage");

            var voyage = new Voyage { };
            voyage.IdDestination = ConsoleSaisie.SaisirEntierObligatoire("IdDestination");
            voyage.IdAgence = ConsoleSaisie.SaisirEntierObligatoire("IdAgence");
            voyage.DateAller = ConsoleSaisie.SaisirDateObligatoire("Date Aller : ");
            voyage.DateRetour = ConsoleSaisie.SaisirDateObligatoire("Date Retour : ");
            voyage.PlacesDisponibles = ConsoleSaisie.SaisirEntierObligatoire("Places disponibles : ");
            voyage.TarifToutCompris = ConsoleSaisie.SaisirDecimalObligatoire("Tarif tout compris : ");
            

            using (var bd = Application.GetBaseDonnees())
            {
                bd.Voyages.Add(voyage);
                bd.SaveChanges();
            }
        }

        private void SupprimerVoyage()
        {
            ConsoleHelper.AfficherEntete("Supprimer un voyage");
            var liste = Application.GetBaseDonnees().Voyages.ToList();

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero id du voyage : ");

            using (var sup = Application.GetBaseDonnees())
            {
                var voyage = sup.Voyages.SingleOrDefault(x => x.Id == id);

                if (liste.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Aucun voyage pour le moment");
                    return;
                }

                else
                {

                    sup.Voyages.Remove(voyage);
                    sup.SaveChanges();
                }
            }
        }

        private void ModifierVoyage()
        {

        }

        private void RechercherVoyage()
        {
            ConsoleHelper.AfficherEntete("Rechercher un voyage");

            var destination = ConsoleSaisie.SaisirChaineObligatoire("Destination du voyage recherché : ");
            var dateAller = ConsoleSaisie.SaisirChaineObligatoire("Date aller du voyage recherché : ");
            var dateRetour = ConsoleSaisie.SaisirChaineObligatoire("Date retour du voyage recherché : ");


            using (var recherche = Application.GetBaseDonnees())
            {
                var liste = recherche.Destinations.Where(x => x.Continent.Contains(destination));
                ConsoleHelper.AfficherListe(liste, strategieAffichageGestionVoyages);
            }   
        }

    }
}
