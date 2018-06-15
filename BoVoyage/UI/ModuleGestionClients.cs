﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Dal;
using BoVoyage.Framework.UI;
using BoVoyage.Metiers;
namespace BoVoyage.UI
{
    public class ModuleGestionClients
    {
        private static readonly List<InformationAffichage> strategieAffichageGestionDossiersClients =
            new List<InformationAffichage>
            {
                InformationAffichage.Creer<Client>(x=>x.Id, "Id", 3),
                InformationAffichage.Creer<Client>(x=>x.Civilite, "Civilite", 4),
                InformationAffichage.Creer<Client>(x=>x.Nom, "Nom", 20),
                InformationAffichage.Creer<Client>(x=>x.Prenom, "Prenom", 20),
                InformationAffichage.Creer<Client>(x=>x.Adresse, "Adresse", 50),
                InformationAffichage.Creer<Client>(x=>x.Telephone, "Telephone", 10),
                InformationAffichage.Creer<Client>(x=>x.Email, "Email", 50),
                InformationAffichage.Creer<Client>(x=>x.DateNaissance, "DateNaissance", 10),
                InformationAffichage.Creer<Client>(x=>x.Age, "Age", 3),
            };

        private Menu menu;
        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des clients");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les clients")
            {
                FonctionAExecuter = this.AfficherClients
            });
            this.menu.AjouterElement(new ElementMenu("2", "Créer un nouveau client")
            {
                FonctionAExecuter = this.AjouterClient
            });
            this.menu.AjouterElement(new ElementMenu("3", "Modifier un client")
            {
                FonctionAExecuter = this.ModifierClient
            });
            this.menu.AjouterElement(new ElementMenu("4", "Supprimer un client")
            {
                FonctionAExecuter = this.SupprimerClient
            });
            this.menu.AjouterElement(new ElementMenu("5", "Rechercher un client")
            {
                FonctionAExecuter = this.RechercherClient
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

        private void AfficherClients()
        {
            ConsoleHelper.AfficherEntete("Clients");

            var liste = Application.GetBaseDonnees().Clients.ToList();
            ConsoleHelper.AfficherListe(liste, strategieAffichageGestionDossiersClients);
        }

        public void AjouterClient()
        {
            ConsoleHelper.AfficherEntete("Nouveau client");

            var client = new Client
            {
                Civilite = ConsoleSaisie.SaisirChaineObligatoire("Entrez votre civilité : "),
                Nom = ConsoleSaisie.SaisirChaineObligatoire("Nom : "),
                Prenom = ConsoleSaisie.SaisirChaineObligatoire("Prénom : "),
                Adresse = ConsoleSaisie.SaisirChaineObligatoire("Adresse : "),
                Telephone = ConsoleSaisie.SaisirChaineObligatoire("Telephone : "),
                DateNaissance = ConsoleSaisie.SaisirDateObligatoire("Date de naissance : "),
                Age = ConsoleSaisie.SaisirEntierObligatoire("Age : "),
                Email = ConsoleSaisie.SaisirChaineObligatoire("Email : ")

            };

            using (var bd = Application.GetBaseDonnees())
            {
                bd.Clients.Add(client);
                bd.SaveChanges();
            }
        }

        private void SupprimerClient()
        {
            ConsoleHelper.AfficherEntete("Supprimer un client");
            var liste = Application.GetBaseDonnees().Clients.ToList();

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero id: ");

            using (var sup = Application.GetBaseDonnees())
            {
                var client = sup.Clients.Single(x => x.Id == id);
                sup.Clients.Remove(client);
                sup.SaveChanges();
            }
        }

        private void RechercherClient()
        {
            ConsoleHelper.AfficherEntete("Rechercher un client");

            var nom = ConsoleSaisie.SaisirChaineObligatoire("Nom du client recherché : ");

            using (var recherche = Application.GetBaseDonnees())
            {
                var liste = recherche.Clients.Where(x => x.Nom.Contains(nom));
            }
        }

        private void ModifierClient()
        {

        }
    }
}
