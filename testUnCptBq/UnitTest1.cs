using libCptBq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace testUnCptBq
{
    [TestClass]
    public class TestCompte
    {
        [TestMethod]
        public void ClasseCompteExiste()
        {
            //Arranger
            System.Type compteType = typeof(Compte);
            //Auditer
            Assert.IsNotNull(compteType, "La classe Compte n'existe pas.");
        }
        [TestMethod]
        public void ConstructeurCompteExiste()
        {
            // Arranger
            Type compteType = typeof(Compte);
            Type[] parametersTypes = new Type[] { typeof(int), typeof(string), typeof(decimal), typeof(decimal) };

            // Agir
            ConstructorInfo constructeur = compteType.GetConstructor(parametersTypes);

            // Assert
            Assert.IsNotNull(constructeur, "Le constructeur Compte(string numero, string nom, decimal solde, decimal decouvertAutorise) n'existe pas.");


        }
        [TestMethod]
        public void ConstructeurSansParametres_InitialiseCorrectement()
        {
            // Arranger
            Compte compte = new Compte();

            // Assert
            Assert.AreEqual(0, compte.Numero, "Le numéro doit être initialisé à 0");
            Assert.AreEqual(0, compte.DecouvertAutorise, "Le découvert autorisé doit être initialisé à 0");
            Assert.AreEqual("", compte.Nom, "le nom doit être vide");
            Assert.AreEqual(0, compte.Solde, "Le solde doit être initialisé à 0");
        }
        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arranger
            Compte compte = new Compte
            {
                Numero = 123456,
                Nom = "toto",
                Solde = 1000.50m,
                DecouvertAutorise = -500.00m
            };

            string expected = "numero: 123456 nom: toto solde: 1000,50 decouvert autorisé: -500,00";

            // Agir
            string result = compte.ToString();

            // Assert
            Assert.AreEqual(expected, result, "La méthode ToString() ne retourne pas le format attendu.");
        }
        public void ToString_ReturnsFormatNull()
        {
            // Arranger
            Compte compte = new Compte
            {
                Numero = 0,
                Nom = "",
                Solde = 0.00m,
                DecouvertAutorise = 0.00m
            };

            string expected = "numero: 0 nom:  solde: 0,00 decouvert autorisé: 0,00";

            // Agir
            string result = compte.ToString();

            // Assert
            Assert.AreEqual(expected, result, "La méthode ToString() ne retourne pas le format attendu.");
        }
        [TestMethod]
        public void Crediter_AugmenteSolde_CorrectementTested()
        {
            // Arranger
            Compte compte = new Compte(1, "Test", 1000m, 500m);
            decimal montantCredit = 500m;
            decimal soldeAttendu = 1500m;

            // Agir
            compte.Crediter(montantCredit);

            // Assert
            Assert.AreEqual(soldeAttendu, compte.Solde, "Le solde n'a pas été correctement crédité");
        }

        [TestMethod]
        public void Crediter_AvecMontantNegatif_AugmenteSoldeTested()
        {
            // Arranger
            Compte compte = new Compte(2, "Test2", 1000m, 500m);
            decimal montantCredit = -200m;
            decimal soldeInitial = compte.Solde;

            // Agir
            compte.Crediter(montantCredit);

            // Assert
            Assert.AreEqual(soldeInitial, compte.Solde, "Le solde ne devrait pas changer lors d'un crédit négatif");
        }

        [TestMethod]
        public void Crediter_AvecZero_NePasChanferSoldeTested()
        {
            // Arranger
            Compte compte = new Compte(3, "Test3", 1000m, 500m);
            decimal montantCredit = 0m;
            decimal soldeAttendu = 1000m;

            // Agir
            compte.Crediter(montantCredit);

            // Assert
            Assert.AreEqual(soldeAttendu, compte.Solde, "Le solde ne devrait pas changer lors d'un crédit de zéro");
        }
        [TestMethod]
        public void Transferer_MontantValideEntreSoldesSuffisants_TransfertReussiTested()
        {
            // Arranger
            Compte compteSource = new Compte(1, "Source", 1000m, 500m);
            Compte compteDestination = new Compte(2, "Destination", 500m, 500m);
            decimal montantTransfert = 300m;

            // Agir
            bool resultat = compteSource.Transferer(montantTransfert, compteDestination);

            // Assert
            Assert.IsTrue(resultat, "Le transfert aurait dû réussir");
            Assert.AreEqual(700m, compteSource.Solde, "Le solde du compte source n'a pas été correctement débité");
            Assert.AreEqual(800m, compteDestination.Solde, "Le solde du compte destination n'a pas été correctement crédité");
        }

        [TestMethod]
        public void Transferer_MontantSuperieurAuSoldeDisponible_TransfertEchoueTested()
        {
            // Arranger
            Compte compteSource = new Compte(3, "Source", 1000m, 500m);
            Compte compteDestination = new Compte(4, "Destination", 500m, 500m);
            decimal montantTransfert = 1600m; // Supérieur au solde + découvert autorisé

            // Agir
            bool resultat = compteSource.Transferer(montantTransfert, compteDestination);

            // Assert
            Assert.IsFalse(resultat, "Le transfert aurait dû échouer");
            Assert.AreEqual(1000m, compteSource.Solde, "Le solde du compte source n'aurait pas dû changer");
            Assert.AreEqual(500m, compteDestination.Solde, "Le solde du compte destination n'aurait pas dû changer");
        }

        [TestMethod]
        public void Transferer_MontantNul_TransfertReussiSansChangementTested()
        {
            // Arranger
            Compte compteSource = new Compte(5, "Source", 1000m, 500m);
            Compte compteDestination = new Compte(6, "Destination", 500m, 500m);
            decimal montantTransfert = 0m;

            // Agir
            bool resultat = compteSource.Transferer(montantTransfert, compteDestination);

            // Assert
            Assert.IsFalse(resultat, "Le transfert d'un montant nul devrait échouer");
            Assert.AreEqual(1000m, compteSource.Solde, "Le solde du compte source n'aurait pas dû changer");
            Assert.AreEqual(500m, compteDestination.Solde, "Le solde du compte destination n'aurait pas dû changer");
        }

        [TestMethod]
        public void Transferer_MontantNegatif_TransfertEchoueTested()
        {
            // Arranger
            Compte compteSource = new Compte(7, "Source", 1000m, 500m);
            Compte compteDestination = new Compte(8, "Destination", 500m, 500m);
            decimal montantTransfert = -200m;

            // Agir
            bool resultat = compteSource.Transferer(montantTransfert, compteDestination);

            // Assert
            Assert.IsFalse(resultat, "Le transfert d'un montant négatif devrait échouer");
            Assert.AreEqual(1000m, compteSource.Solde, "Le solde du compte source n'aurait pas dû changer");
            Assert.AreEqual(500m, compteDestination.Solde, "Le solde du compte destination n'aurait pas dû changer");
        }
        [TestMethod]
        public void Superieur_SoldeSuperieur_RetourneTrueTested()
        {
            // Arrange
            Compte compte1 = new Compte(1, "Compte1", 1000m, 500m);
            Compte compte2 = new Compte(2, "Compte2", 500m, 500m);

            // Agir
            bool resultat = compte1.Superieur(compte2);

            // Assert
            Assert.IsTrue(resultat, "Le compte1 devrait être supérieur au compte2");
        }

        [TestMethod]
        public void Superieur_SoldeInferieur_RetourneFalseTested()
        {
            // Arrange
            Compte compte1 = new Compte(1, "Compte1", 500m, 500m);
            Compte compte2 = new Compte(2, "Compte2", 1000m, 500m);

            // Agir
            bool resultat = compte1.Superieur(compte2);

            // Assert
            Assert.IsFalse(resultat, "Le compte1 ne devrait pas être supérieur au compte2");
        }

        [TestMethod]
        public void Superieur_SoldesEgaux_RetourneFalseTested()
        {
            // Arrange
            Compte compte1 = new Compte(1, "Compte1", 1000m, 500m);
            Compte compte2 = new Compte(2, "Compte2", 1000m, 500m);

            // Agir
            bool resultat = compte1.Superieur(compte2);

            // Assert
            Assert.IsFalse(resultat, "Les comptes ayant des soldes égaux ne devraient pas être considérés comme supérieurs");
        }

        [TestMethod]
        public void Superieur_ComparaisonAvecSoldeNegatif_RetourneTrueTested()
        {
            // Arrange
            Compte compte1 = new Compte(1, "Compte1", 0m, 500m);
            Compte compte2 = new Compte(2, "Compte2", -100m, 500m);

            // Agir
            bool resultat = compte1.Superieur(compte2);

            // Assert
            Assert.IsTrue(resultat, "Le compte1 avec un solde de 0 devrait être supérieur au compte2 avec un solde négatif");
        }

        [TestMethod]
        public void ClasseBanqueExiste()
        {
            //Arranger
            Type BanqueType = typeof(Banque);
            //Auditer
            Assert.IsNotNull(BanqueType, "La classe Banque n'existe pas.");
        }

        [TestMethod]
        public void ConstructeurBanqueExiste()
        {
            // Arranger  
            Type BanqueType = typeof(Banque);

            // Agir  
            ConstructorInfo constructeur = BanqueType.GetConstructor(Type.EmptyTypes);

            // Assert  
            Assert.IsNotNull(constructeur, "Le constructeur Banque n'existe pas.");


        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormatBanque()
        {
            // Arranger
            Banque banque = new Banque();
            Compte compte = new Compte
            {
                Numero = 123456,
                Nom = "toto",
                Solde = 1000.50m,
                DecouvertAutorise = -500.00m
            };
            banque.AjouteCompte(compte);

            string expected = "numero: 123456 nom: toto solde: 1000,50 decouvert autorisé: -500,00";

            // Agir
            string result = banque.ToString();

            // Assert
            Assert.AreEqual(expected, result, "La méthode ToString() ne retourne pas le format attendu.");
        }

        [TestMethod]
        public void RendCompte_CompteExiste_RetourneCompteTested()
        {
            // Arranger
            Banque banque = new Banque();
            Compte compte = new Compte(123456, "toto", 1000.50m, -500.00m);
            banque.AjouteCompte(compte);
            // Agir
            Compte resultat = banque.RendCompte(123456);
            // Assert
            Assert.IsNotNull(resultat, "Le compte devrait être trouvé");
            Assert.AreEqual(compte, resultat, "Le compte retourné n'est pas le bon");
        }

        [TestMethod]
        public void RetourneTypeDeMouvement()
        {
            // Arranger
            Banque banque = new Banque();
            Type_ type1 = new Type_("001", "TypeA", '+');
            Type_ type2 = new Type_("002", "TypeB", '-');
            banque.AjouterType(type1);
            banque.AjouterType(type2);

            // Agir
            string resultat = banque.GetTypes("TypeA");

            // Assert
            Assert.IsTrue(resultat.Contains(type1.ToString()), "Le type avec le libellé spécifié devrait être inclus dans le résultat");
            Assert.IsFalse(resultat.Contains(type2.ToString()), "Le type avec un libellé différent ne devrait pas être inclus dans le résultat");
        }

        [TestMethod]
        public void ClasseTypeExiste()
        {
            //Arranger
            Type TypeType = typeof(Type_);
            //Auditer
            Assert.IsNotNull(TypeType, "La classe Type n'existe pas.");
        }

        [TestMethod]
        public void ClasseMouvementExiste()
        {
            //Arranger
            Type MouvementType = typeof(Mouvement);
            //Auditer
            Assert.IsNotNull(MouvementType, "La classe Mouvement n'existe pas.");
        }

        [TestMethod]
        public void ClasseType_Existe()
        {
            //Arranger
            Type TypeType = typeof(Type_);
            //Auditer
            Assert.IsNotNull(TypeType, "La classe Type_ n'existe pas.");
        }

        [TestMethod]
        public void ConstructeurType_Existe()
        {
            // Arranger  
            Type TypeType = typeof(Type_);
            // Agir  
            ConstructorInfo constructeur = TypeType.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(char) });
            // Assert  
            Assert.IsNotNull(constructeur, "Le constructeur Type_(string code, string libelle, char sens) n'existe pas.");
        }
        [TestMethod]
        public void ToString_ReturnsCorrectFormatType()
        {
            // Arranger
            Type_ type = new Type_("001", "TypeA", '+');
            string expected = "Code: 001, Libelle: TypeA, Sens: +";

            // Agir
            string result = type.ToString();

            // Assert
            Assert.AreEqual(expected, result, "La méthode ToString() ne retourne pas le format attendu.");
        }

        public void ConstructeurMouvement_Existe()
        {
            // Arranger  
            Type MouvementType = typeof(Mouvement);
            // Agir  
            ConstructorInfo constructeur = MouvementType.GetConstructor(new Type[] { typeof(int), typeof(DateTime), typeof(decimal), typeof(string), typeof(Type_) });
            // Assert  
            Assert.IsNotNull(constructeur, "Le constructeur Mouvement(int numero, DateTime date, decimal montant, string motif, Type_ type) n'existe pas.");
        }
    }
}