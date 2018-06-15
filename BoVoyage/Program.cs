using BoVoyage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.Demarrer();
        }
    }
}
/* 
Toutes les options du module client fonctionnent à part la partie "modifier". Il est possible d'afficher, de creer et de supprimer dans les modules de gestion des dossiers de 
réservation et de voyages. Cependant si l'utilisateur saisit des identifiants qui ne correspondent à aucune ligne des tables de notre base de données il fera face a des exceptions.
Nous avons complété certaines tables pour tester nos différents modules et vérifier le bon fonctionnement des méthodes. L'affichage des listes dans la console n'est pas parfaite et
doit être corrigé. Des retours au menu précédent doivent être ajoutés. L'age n'est pas calculé automatiquement à partir de la date de naissance. On peut imaginer créer également un
module de gestion des destinations.
/*/