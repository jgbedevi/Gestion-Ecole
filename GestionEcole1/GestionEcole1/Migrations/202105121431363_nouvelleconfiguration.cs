namespace GestionEcole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nouvelleconfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.College",
                c => new
                    {
                        CodeCollege = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 100, unicode: false),
                        Adresse = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CodeCollege);
            
            CreateTable(
                "dbo.Departement",
                c => new
                    {
                        Code_Departement = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50, unicode: false),
                        CollegeId = c.Int(nullable: false),
                        EnseignantChef_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Code_Departement)
                .ForeignKey("dbo.Enseignant", t => t.EnseignantChef_Id)
                .ForeignKey("dbo.College", t => t.CollegeId, cascadeDelete: true)
                .Index(t => t.CollegeId)
                .Index(t => t.EnseignantChef_Id);
            
            CreateTable(
                "dbo.Enseignant",
                c => new
                    {
                        NumEnseignant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 50, unicode: false),
                        Tel = c.Int(nullable: false),
                        Mail = c.String(nullable: false, maxLength: 100, unicode: false),
                        DatePriseDeFonction = c.DateTime(nullable: false),
                        Indice = c.Int(nullable: false),
                        MatiereId = c.Int(nullable: false),
                        DepartementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumEnseignant)
                .ForeignKey("dbo.Departement", t => t.DepartementId, cascadeDelete: true)
                .ForeignKey("dbo.Matiere", t => t.MatiereId)
                .Index(t => t.MatiereId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Matiere",
                c => new
                    {
                        NumMatiere = c.Int(nullable: false, identity: true),
                        LibelleCours = c.String(nullable: false, maxLength: 50, unicode: false),
                        SalleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumMatiere)
                .ForeignKey("dbo.Salle", t => t.SalleId, cascadeDelete: true)
                .Index(t => t.SalleId);
            
            CreateTable(
                "dbo.Etudiant",
                c => new
                    {
                        NumEtudiant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 50, unicode: false),
                        Tel = c.Int(nullable: false),
                        Mail = c.String(nullable: false, maxLength: 100, unicode: false),
                        AnneEntree = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NumEtudiant);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NumNote = c.Int(nullable: false, identity: true),
                        NoteControle = c.Double(nullable: false),
                        MatiereId = c.Int(nullable: false),
                        EtudiantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumNote)
                .ForeignKey("dbo.Etudiant", t => t.EtudiantId)
                .ForeignKey("dbo.Matiere", t => t.MatiereId)
                .Index(t => t.MatiereId)
                .Index(t => t.EtudiantId);
            
            CreateTable(
                "dbo.Salle",
                c => new
                    {
                        No_Salle = c.Int(nullable: false, identity: true),
                        NomSalle = c.String(nullable: false, maxLength: 50, unicode: false),
                        Nb_Place = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.No_Salle);
            
            CreateTable(
                "dbo.EtudiantMatieres",
                c => new
                    {
                        Etudiant_Id = c.Int(nullable: false),
                        Matiere_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etudiant_Id, t.Matiere_Id })
                .ForeignKey("dbo.Etudiant", t => t.Etudiant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matiere", t => t.Matiere_Id, cascadeDelete: true)
                .Index(t => t.Etudiant_Id)
                .Index(t => t.Matiere_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departement", "CollegeId", "dbo.College");
            DropForeignKey("dbo.Departement", "EnseignantChef_Id", "dbo.Enseignant");
            DropForeignKey("dbo.Enseignant", "MatiereId", "dbo.Matiere");
            DropForeignKey("dbo.Matiere", "SalleId", "dbo.Salle");
            DropForeignKey("dbo.Notes", "MatiereId", "dbo.Matiere");
            DropForeignKey("dbo.Notes", "EtudiantId", "dbo.Etudiant");
            DropForeignKey("dbo.EtudiantMatieres", "Matiere_Id", "dbo.Matiere");
            DropForeignKey("dbo.EtudiantMatieres", "Etudiant_Id", "dbo.Etudiant");
            DropForeignKey("dbo.Enseignant", "DepartementId", "dbo.Departement");
            DropIndex("dbo.EtudiantMatieres", new[] { "Matiere_Id" });
            DropIndex("dbo.EtudiantMatieres", new[] { "Etudiant_Id" });
            DropIndex("dbo.Notes", new[] { "EtudiantId" });
            DropIndex("dbo.Notes", new[] { "MatiereId" });
            DropIndex("dbo.Matiere", new[] { "SalleId" });
            DropIndex("dbo.Enseignant", new[] { "DepartementId" });
            DropIndex("dbo.Enseignant", new[] { "MatiereId" });
            DropIndex("dbo.Departement", new[] { "EnseignantChef_Id" });
            DropIndex("dbo.Departement", new[] { "CollegeId" });
            DropTable("dbo.EtudiantMatieres");
            DropTable("dbo.Salle");
            DropTable("dbo.Notes");
            DropTable("dbo.Etudiant");
            DropTable("dbo.Matiere");
            DropTable("dbo.Enseignant");
            DropTable("dbo.Departement");
            DropTable("dbo.College");
        }
    }
}
