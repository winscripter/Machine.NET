using FMA3Generator;

string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");
if (Directory.Exists(outputDirectory))
{
    Console.WriteLine($"Please delete '{outputDirectory}' first.");
    return;
}

var generator = new Generator();

Generate("Vfmadd", generator.VfmaddPS);
Generate2("Vfmadd", "Scalar", generator.VfmaddSS);

Generate("Vfmaddsub", generator.Vfmaddsub);

Generate("Vfmsub", generator.VfmsubPS);
Generate2("Vfmsub", "Scalar", generator.VfmsubSS);

Generate("Vfmsubadd", generator.Vfmsubadd);

Generate("Vfnmadd", generator.VfnmaddPS);
Generate2("Vfnmadd", "Scalar", generator.VfnmaddSS);

Generate("Vfnmsub", generator.VfnmsubPS);
Generate2("Vfnmsub", "Scalar", generator.VfnmsubSS);

void Generate(string nestedFolder, Action generate)
{
    string result = Path.Combine(outputDirectory, nestedFolder);
    Directory.CreateDirectory(result);

    generator.OutputDirectory = result;
    generate();
}

void Generate2(string nestedFolder, string nestedFolder2, Action generate)
{
    string result = Path.Combine(outputDirectory, nestedFolder, nestedFolder2);
    Directory.CreateDirectory(result);

    generator.OutputDirectory = result;
    generate();
}
