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
        DiagramObjectCollection<Connector> connectors = default!;
        ObservableCollection<string> shapes = default!;
        ObservableCollection<string> connectorsShape = default!;

        private bool collapseNavMenu = true;
        private bool expandNavMenu = false;
        private bool collapseConnnectors = false;
        private bool expandConnnectorsShape = false;

        private string? NavMenuCollapseCss => collapseNavMenu ? "collapse" : null;
        private string? NavConnectorsCollapseCss => collapseConnnectors ? "collapse" : null;

        protected override void OnInitialized()
        {
            nodes = new DiagramObjectCollection<Node>();
            connectors = new DiagramObjectCollection<Connector>();
            //initializing palettes
            InitializePalettes();
            base.OnInitialized();
        }

        private void ExpandNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
            expandNavMenu = !expandNavMenu;
        }

        private void ExpandOrCollapseConnectorsArea()
        {
            collapseConnnectors = !collapseConnnectors;
            expandConnnectorsShape = !expandConnnectorsShape;
        }

        private void InitializePalettes()
        {
            var t = EnumHelper.GetEnumItems<BasicShapeType>();
            var c = EnumHelper.GetEnumItems<ConnectorSegmentType>();
            shapes = new ObservableCollection<string>(t.Select(x => x.ToString()));
            connectorsShape = new ObservableCollection<string>(c.Select(x => x.ToString()));
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

        private void CreateConnector(string name)
        {
            var conShape = EnumHelper.GetValueOf<ConnectorSegmentType>(name);
            Console.WriteLine(conShape.ToString(), "connector selected");
            Connector connector = new Connector
            {
                ID = Guid.NewGuid().ToString(),
                SourcePoint = new DiagramPoint() { X = 100, Y = 100 },
                TargetPoint = new DiagramPoint() { X = 200, Y = 200 },
                Constraints = ConnectorConstraints.Default | ConnectorConstraints.DragSegmentThumb,
                
            };
            connectors.Add(connector);
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
            export.Margin = new Margin() { Left = 10, Right = 10, Bottom = 10, Top = 10 };

            await diagram.ExportAsync("diagram", DiagramExportFormat.PNG, export);
        }

        private async void PrindOrExportPdf()
        {
            double left = 10;
            double top = 30;
            double right = 10;
            double bottom = 10;
            double width = 480;
            double height = 500;
            bool multiplePage = true;
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
