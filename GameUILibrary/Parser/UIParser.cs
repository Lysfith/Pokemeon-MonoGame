using GameUILibrary.Components;
using GameUILibrary.Components.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameUILibrary.Parser
{
    internal class UIParser
    {
        private XDocument _doc;

        public UIParser(string document)
        {
            _doc = XDocument.Load(document);
            
        }

        public UI Parse(Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            var root = _doc.Root;

            var ui = CreateUI(root, game);

            var layers = root.Elements();

            foreach (var layerNode in layers)
            {
                var layer = CreateLayer(layerNode, game, texts, callbacks);
                ui.AddLayer(layer);
            }

            return ui;
        }

        private UI CreateUI(XElement node, Game game)
        {
            var ui = new UI(game);

            if (node.Name != "UI")
            {
                throw new Exception("XML: UI tag");
            }

            if(node.HasAttributes)
            {
                foreach(var attr in node.Attributes())
                {
                    var name = attr.Name.LocalName;
                   
                }
            }

            return ui;
        }

        private UILayer CreateLayer(XElement node, Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            var layer = new UILayer(game);

            if (node.Name != "UILayer")
            {
                throw new Exception("XML: UILayer tag");
            }

            if (node.HasAttributes)
            {
                foreach (var attr in node.Attributes())
                {
                    var name = attr.Name.LocalName;
                    var value = attr.Value;
                    Color color = Color.Transparent;
                    switch (name)
                    {
                        case "x":
                            if (value.IndexOf("%") != -1)
                            {
                                var percentX = int.Parse(value.Trim(new char[] { '%', ' ' }));
                                layer.X = percentX * game.GraphicsDevice.Viewport.Width / 100;
                            }
                            else
                            {
                                var x = int.Parse(value);
                                layer.X = x;
                            }
                            break;
                        case "y":
                            if (value.IndexOf("%") != -1)
                            {
                                var percentY = int.Parse(value.Trim(new char[] { '%', ' ' }));
                                layer.Y = percentY * game.GraphicsDevice.Viewport.Height / 100;
                            }
                            else
                            {
                                var y = int.Parse(value);
                                layer.Y = y;
                            }
                            break;
                        case "width":
                            if (value.IndexOf("%") != -1)
                            {
                                var percentWidth = int.Parse(value.Trim(new char[] { '%', ' ' }));
                                layer.Width = percentWidth * game.GraphicsDevice.Viewport.Width / 100;
                            }
                            else
                            {
                                var width = int.Parse(value);
                                layer.Width = width;
                            }
                            break;
                        case "height":
                            if (value.IndexOf("%") != -1)
                            {
                                var percentHeight = int.Parse(value.Trim(new char[] { '%', ' ' }));
                                layer.Height = percentHeight * game.GraphicsDevice.Viewport.Height / 100;
                            }
                            else
                            {
                                var height = int.Parse(value);
                                layer.Height = height;
                            }
                            break;
                        case "colorR":
                            var colorR = byte.Parse(value);
                            color = layer.BackgroundColor;
                            color.R = colorR;
                            layer.BackgroundColor = color;
                            break;
                        case "colorG":
                            var colorG = byte.Parse(value);
                            color = layer.BackgroundColor;
                            color.G = colorG;
                            layer.BackgroundColor = color;
                            break;
                        case "colorB":
                            var colorB = byte.Parse(value);
                            color = layer.BackgroundColor;
                            color.B = colorB;
                            layer.BackgroundColor = color;
                            break;
                        case "colorA":
                            var colorA = byte.Parse(value);
                            color = layer.BackgroundColor;
                            color.A = colorA;
                            layer.BackgroundColor = color;
                            break;
                    }
                }
            }

            var children = node.Elements();

            foreach (var childNode in children)
            {
                var child = CreateControl(childNode, game, texts, callbacks);

                if (child != null)
                {
                    layer.AddControl(child);
                }
            }

            return layer;
        }

        private UIPanel CreateUIPanel(XElement node, Game game)
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;
            Color color = Color.Transparent;

            if (node.Name == "UIPanel")
            {
                if (node.HasAttributes)
                {
                    foreach (var attr in node.Attributes())
                    {
                        var name = attr.Name.LocalName;
                        var value = attr.Value;

                        switch (name)
                        {
                            case "x":
                                x = int.Parse(value);
                                break;
                            case "y":
                                y = int.Parse(value);
                                break;
                            case "width":
                                width = int.Parse(value);
                                break;
                            case "height":
                                height = int.Parse(value);
                                break;
                            case "colorR":
                                var colorR = byte.Parse(value);
                                color.R = colorR;
                                break;
                            case "colorG":
                                var colorG = byte.Parse(value);
                                color.G = colorG;
                                break;
                            case "colorB":
                                var colorB = byte.Parse(value);
                                color.B = colorB;
                                break;
                            case "colorA":
                                var colorA = byte.Parse(value);
                                color.A = colorA;
                                break;
                        }
                    }
                }

                var control = new UIPanel(game.Content, new Point(x, y), width, height, color);

                return control;
            }

            return null;
        }

        private UILabel CreateUILabel(XElement node, Game game, Dictionary<string, string> texts)
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;
            Color color = Color.Transparent;
            bool textCentered = false;
            string text = null;

            if (node.Name == "UILabel")
            {
                if (node.HasAttributes)
                {
                    foreach (var attr in node.Attributes())
                    {
                        var name = attr.Name.LocalName;
                        var value = attr.Value;

                        switch (name)
                        {
                            case "x":
                                x = int.Parse(value);
                                break;
                            case "y":
                                y = int.Parse(value);
                                break;
                            case "width":
                                width = int.Parse(value);
                                break;
                            case "height":
                                height = int.Parse(value);
                                break;
                            case "colorR":
                                var colorR = byte.Parse(value);
                                color.R = colorR;
                                break;
                            case "colorG":
                                var colorG = byte.Parse(value);
                                color.G = colorG;
                                break;
                            case "colorB":
                                var colorB = byte.Parse(value);
                                color.B = colorB;
                                break;
                            case "colorA":
                                var colorA = byte.Parse(value);
                                color.A = colorA;
                                break;
                            case "textcentered":
                                textCentered = bool.Parse(value);
                                break;
                            case "text":
                                text = value;
                                break;

                        }
                    }
                }

                var control = new UILabel(game.Content, new Point(x, y), width, height, node.Value);
                control.TextCentered = textCentered;
                if (texts != null && texts.ContainsKey(text))
                {
                    control.Text = texts[text];
                }
                return control;
            }

            return null;
        }

        private UIButton CreateUIButton(XElement node, Game game)
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;
            Color color = Color.Transparent;

            if (node.Name == "UIButton")
            {
                if (node.HasAttributes)
                {
                    foreach (var attr in node.Attributes())
                    {
                        var name = attr.Name.LocalName;
                        var value = attr.Value;

                        switch (name)
                        {
                            case "x":
                                x = int.Parse(value);
                                break;
                            case "y":
                                y = int.Parse(value);
                                break;
                            case "width":
                                width = int.Parse(value);
                                break;
                            case "height":
                                height = int.Parse(value);
                                break;
                            case "colorR":
                                var colorR = byte.Parse(value);
                                color.R = colorR;
                                break;
                            case "colorG":
                                var colorG = byte.Parse(value);
                                color.G = colorG;
                                break;
                            case "colorB":
                                var colorB = byte.Parse(value);
                                color.B = colorB;
                                break;
                            case "colorA":
                                var colorA = byte.Parse(value);
                                color.A = colorA;
                                break;
                        }
                    }
                }

                var control = new UIButton(game.Content, new Point(x, y), width, height, node.Value);

                return control;
            }

            return null;
        }

        private UIImage CreateUIImage(XElement node, Game game)
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;
            Color color = Color.Transparent;
            string path = "";

            if (node.Name == "UIImage")
            {
                if (node.HasAttributes)
                {
                    foreach (var attr in node.Attributes())
                    {
                        var name = attr.Name.LocalName;
                        var value = attr.Value;

                        switch (name)
                        {
                            case "x":
                                x = int.Parse(value);
                                break;
                            case "y":
                                y = int.Parse(value);
                                break;
                            case "width":
                                width = int.Parse(value);
                                break;
                            case "height":
                                height = int.Parse(value);
                                break;
                            case "colorR":
                                var colorR = byte.Parse(value);
                                color.R = colorR;
                                break;
                            case "colorG":
                                var colorG = byte.Parse(value);
                                color.G = colorG;
                                break;
                            case "colorB":
                                var colorB = byte.Parse(value);
                                color.B = colorB;
                                break;
                            case "colorA":
                                var colorA = byte.Parse(value);
                                color.A = colorA;
                                break;
                            case "src":
                                path = value;
                                break;
                        }
                    }
                }

                var control = new UIImage(game.Content, new Point(x, y), width, height, path);

                return control;
            }

            return null;
        }

        private UIBaseElement CreateControlFile(XElement node, Game game)
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;
            Color color = Color.Transparent;

            if (node.Name == "ControlFile")
            {
                if (node.HasAttributes)
                {
                    foreach (var attr in node.Attributes())
                    {
                        var name = attr.Name.LocalName;
                        var value = attr.Value;

                        switch (name)
                        {
                            case "x":
                                x = int.Parse(value);
                                break;
                            case "y":
                                y = int.Parse(value);
                                break;
                            case "width":
                                width = int.Parse(value);
                                break;
                            case "height":
                                height = int.Parse(value);
                                break;
                            case "colorR":
                                var colorR = byte.Parse(value);
                                color.R = colorR;
                                break;
                            case "colorG":
                                var colorG = byte.Parse(value);
                                color.G = colorG;
                                break;
                            case "colorB":
                                var colorB = byte.Parse(value);
                                color.B = colorB;
                                break;
                            case "colorA":
                                var colorA = byte.Parse(value);
                                color.A = colorA;
                                break;
                        }
                    }
                }

                var c = new UIParser(node.Value);
                var control = c.ParseControl(game, null, null);

                control.Position = new Point(x, y);
                control.Width = width;
                control.Height = height;

                return control;
            }

            return null;
        }

        public UIBaseElement ParseControl(Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            var root = _doc.Root;

            return CreateControl(root, game, texts, callbacks);
        }

        private UIBaseElement CreateControl(XElement node, Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            UIBaseElement element = null;
            switch(node.Name.LocalName)
            {
                case "UIPanel":
                    element = CreateUIPanel(node, game);
                    break;
                case "UILabel":
                    element = CreateUILabel(node, game, texts);
                    break;
                case "UIButton":
                    element = CreateUIButton(node, game);
                    break;
                case "UIImage":
                    element = CreateUIImage(node, game);
                    break;
                case "ControlFile":
                    element = CreateControlFile(node, game);
                    break;
            }

            //Events
            if (callbacks != null)
            {
                if (node.Attribute("onhoverstart") != null)
                {
                    if (callbacks.ContainsKey(node.Attribute("onhoverstart").Value))
                    {
                        element.OnHoverStart += new EventHandler<EventArgs>(callbacks[node.Attribute("onhoverstart").Value]);
                    }
                }

                if (node.Attribute("onhoverend") != null)
                {
                    if (callbacks.ContainsKey(node.Attribute("onhoverend").Value))
                    {
                        element.OnHoverEnd += new EventHandler<EventArgs>(callbacks[node.Attribute("onhoverend").Value]);
                    }
                }

                if (node.Attribute("ongainfocus") != null)
                {
                    if (callbacks.ContainsKey(node.Attribute("ongainfocus").Value))
                    {
                        element.OnGainFocus += new EventHandler<EventArgs>(callbacks[node.Attribute("ongainfocus").Value]);
                    }
                }
            }

            var children = node.Elements();

            foreach (var childNode in children)
            {
                var child = CreateControl(childNode, game, texts, callbacks);

                if (child != null)
                {
                    element.AddChild(child);
                }
            }

            return element;
        }
    }
}
