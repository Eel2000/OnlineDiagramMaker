using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.Diagram.SymbolPalette;
using OnlineDiagramMaker.Shared.Helpers;
using System.Collections.ObjectModel;

namespace OnlineDiagramMaker.Client.Pages
{
    public partial class WorkSpace : ComponentBase
    {
        SfDiagramComponent? diagram;

        DiagramObjectCollection<Node> nodes = default!;
        ObservableCollection<string> shapes = default!;

        protected override void OnInitialized()
        {
            nodes = new DiagramObjectCollection<Node>();
            //initilize palettes
            InitializePalettes();
            base.OnInitialized();
        }

        private void InitializePalettes()
        {
            var t = EnumHelper.GetEnumItems<BasicShapeType>();
            shapes = new ObservableCollection<string>(t.Select(x => x.ToString()));
        }

        private void CreateBasicForm(string basic)
        {
            var shapeType = EnumHelper.GetValueOf<BasicShapeType>(basic);
            Console.WriteLine(shapeType.ToString());
            Node node = new Node()
            {
                ID = Guid.NewGuid().ToString(),
                OffsetX = 250,
                OffsetY = 250,
                Width = 200,
                Height = 200,
                Style = new ShapeStyle()
                {
                    Fill = "#6495ED",
                    StrokeColor = "white"
                },
                Shape = new BasicShape() { Type = Shapes.Basic, Shape = shapeType }
            };

            nodes.Add(node);
        }

        private void UndoDraw() => diagram?.Undo();
        private void RedoDraw() => diagram?.Redo();


        private async void Exportpng()
        {
            DiagramExportSettings export = new DiagramExportSettings();
            export.Orientation = PageOrientation.Landscape;
            export.Region = DiagramPrintExportRegion.ClipBounds;
            export.PageWidth = 816;
            export.PageHeight = 1054;
            export.Margin = new Margin() { Left=10, Right=10, Bottom=10, Top = 10 };

            await diagram.ExportAsync("diagram", DiagramExportFormat.PNG, export);
        }

        private async void PrindOrExportPdf()
        {
            double left = 10;
            double top = 10;
            double right = 10;
            double bottom = 10;
            double width = 480;
            double height = 500;
            bool multiplePage = true;
            bool showPageBreak = true;
            DiagramPrintExportRegion region = DiagramPrintExportRegion.ClipBounds;
            PageOrientation orientation = PageOrientation.Portrait;

            DiagramPrintSettings print = new DiagramPrintSettings();
            print.PageWidth = width;
            print.PageHeight = height;
            print.Region = region;
            print.FitToPage = multiplePage;
            print.Orientation = orientation;
            print.Margin = new Margin() { Left = left, Top = top, Right = right, Bottom = bottom };
            await diagram.PrintAsync(print);
        }
    }
}
