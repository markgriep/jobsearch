public static class DatabasePathResolver
{



 /*
        Folder layout we’re assuming:

          Parent folder for all my SOLUTIONS
          ├── databases         ---===> folder for dbs of ANY solution
          │   └── JobSearch.db              (real / local data — NOT in repo)
          └── Job Search Application Repo
              ├── db
              │   └── Starter-JobSearch.db (sample DB — in repo)
              └── Program.cs

        Resolution logic:

          1) Go UP one level from the app root
          2) Look for /databases/JobSearch.db
          3) If it exists, use it
          4) Otherwise fall back to /db/Starter-JobSearch.db in the repo
        */


    public static string Resolve(string contentRootPath)
    {

        // Get the db in the One Up from here location
        var externalDb = Path.GetFullPath(
            Path.Combine(contentRootPath, "..", "databases", "JobSearch.db")
        );


        // Does it exist?
        if (File.Exists(externalDb))
            return externalDb;

        

        var x = Path.GetFullPath(
            Path.Combine(contentRootPath, "db", "Starter-JobSearch.db")
        );

        return x;

    }
}
