using CommandLine;

namespace Parse_Get_AppPackage
{
    internal class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input file name.")]
            public string InputFile { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file name.")]
            public string OutputFile { get; set; }
        }

        public class SoftwareDetails
        {
            public string? Name { get; set; }
            public string? Publisher { get; set; }
            public string? Architecture { get; set; }
            public string? ResourceId { get; set; }
            public string? Version { get; set; }
            public string? PackageFullName { get; set; }
            public string? InstallLocation { get; set; }
            public bool? IsFramework { get; set; }
            public string? PackageFamilyName { get; set; }
            public string? PublisherId { get; set; }
            public string? PackageUserInformation { get; set; }
            public bool? IsResourcePackage { get; set; }
            public bool? IsBundle { get; set; }
            public bool? IsDevelopmentMode { get; set; }
            public bool? NonRemovable { get; set; }
            public bool? IsPartiallyStaged { get; set; }
            public string? SignatureKind { get; set; }
            public string? Status { get; set; }

            public void AddData(string input)
            {
                string[] parsedData = input.Split(':');
                switch (parsedData[0].Trim())
                {
                    case "Name":
                        Name = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "Publisher":
                        Publisher = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "Architecture":
                        Architecture = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "ResourceId":
                        ResourceId = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "Version":
                        Version = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "PackageFullName":
                        PackageFullName = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "InstallLocation":
                        InstallLocation = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "IsFramework":
                        IsFramework = parsedData[1] == "True";
                        break;
                    case "PackageFamilyName":
                        PackageFamilyName = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "PublisherId":
                        PublisherId = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "PackageUserInformation":
                        PackageUserInformation = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "IsResourcePackage":
                        IsResourcePackage = parsedData[1] == "True";
                        break;
                    case "IsBundle":
                        IsBundle = parsedData[1] == "True";
                        break;
                    case "IsDevelopmentMode":
                        IsDevelopmentMode = parsedData[1] == "True";
                        break;
                    case "NonRemovable":
                        NonRemovable = parsedData[1] == "True";
                        break;
                    case "IsPartiallyStaged":
                        IsPartiallyStaged = parsedData[1] == "True";
                        break;
                    case "SignatureKind":
                        SignatureKind = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                    case "Status":
                        Status = input.Substring(input.IndexOf(':') + 1).Replace("\"", "\"\"").Trim();
                        break;
                }
            }

            public override string ToString()
            {
                return $"\"{Name}\",\"{Publisher}\",\"{Architecture}\",\"{ResourceId}\",\"{Version}\",\"{PackageFullName}\",\"{InstallLocation}\",\"{IsFramework}\",\"{PackageFamilyName}\",\"{PublisherId}\",\"{PackageUserInformation}\",\"{IsResourcePackage}\",\"{IsBundle}\",\"{IsDevelopmentMode}\",\"{NonRemovable}\",\"{IsPartiallyStaged}\",\"{SignatureKind}\",\"{Status}\"{System.Environment.NewLine}";
            }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => ProcessFiles(opts.InputFile, opts.OutputFile));
        }

        static void ProcessFiles(string inputFile, string outputFile)
        {
            bool firstRow = true;
            System.IO.File.Delete(outputFile);
            System.IO.File.WriteAllText(outputFile, "Name,Publisher,Architecture,ResourceId,Version,PackageFullName,InstallLocation,IsFramework,PackageFamilyName,PublisherId,PackageUserInformation,IsResourcePackage,IsBundle,IsDevelopmentMode,NonRemovable,IsPartiallyStaged,SignatureKind,Status" + System.Environment.NewLine);
            SoftwareDetails details = new SoftwareDetails();
            foreach (var line in System.IO.File.ReadLines(inputFile))
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (!firstRow)
                    {
                        System.IO.File.AppendAllText(outputFile, details.ToString());
                        details = new SoftwareDetails();
                        firstRow = true;
                    }
                }
                else
                {
                    details.AddData(line);
                    firstRow = false;
                }
            }
            if (!firstRow)
            {
                System.IO.File.AppendAllText(outputFile, details.ToString());
                details = new SoftwareDetails();
                firstRow = true;
            }
        }
    }
}
