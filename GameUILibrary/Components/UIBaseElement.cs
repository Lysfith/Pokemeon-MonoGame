using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUILibrary;

namespace GameUILibrary.Components
{
    public class UIBaseElement : IDisposable
    {
        public ContentManager _content;

        public Point Position { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public bool Initialized { get; set; }
        public bool HasFocus { get; set; }
        public bool HasHover { get; set; }
        public bool HasChildFocus { get; set; }

        public bool Enable { get; set; }
        public bool Visible { get; set; }

        public UILayer Layer { get; set; }

        public UIBaseElement Parent { get; set; }
        public List<UIBaseElement> Children { get; set; }

        public event EventHandler<EventArgs> OnHoverStart;
        public event EventHandler<EventArgs> OnHoverEnd;
        public event EventHandler<EventArgs> OnGainFocus;
        public event EventHandler<EventArgs> OnLostFocus;

        public UIBaseElement(ContentManager contentmanager)
        {
            _content = contentmanager;

            Enable = true;
            Visible = true;
            Children = new List<UIBaseElement>();
            Initialized = false;
        }

        public virtual void LoadContent()
        {
            foreach(var child in Children)
            {
                child.LoadContent();
            }

            Initialized = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if(!Initialized && Enable)
            {
                LoadContent();
            }

            if (Enable && Visible)
            {

                var mouseState = Mouse.GetState();

                var bounds = GetLocalBounds();

                if (bounds.X <= mouseState.X && mouseState.X < bounds.X + bounds.Width
                    && bounds.Y <= mouseState.Y && mouseState.Y < bounds.Y + bounds.Height)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        HasFocus = true;
                        if (OnGainFocus != null)
                        {
                            OnGainFocus(this, null);
                        }
                    }
                    HasHover = true;
                    if (OnHoverStart != null)
                    {
                        OnHoverStart(this, null);
                    }
                }
                else
                {
                    if (HasFocus && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        HasFocus = false;

                        if (OnLostFocus != null)
                        {
                            OnLostFocus(this, null);
                        }
                    }

                    if (HasHover && OnHoverEnd != null)
                    {
                        OnHoverEnd(this, null);
                    }

                    HasHover = false;
                }

                foreach (var child in Children)
                {
                    child.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Initialized && Enable)
            {
                LoadContent();
            }

            if (Enable && Visible)
            {
                foreach (var child in Children)
                {
                    child.Draw(gameTime, spriteBatch);
                }
            }

        }

        public void AddChild(UIBaseElement child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(UIBaseElement child)
        {
            if (Children != null && Children.Contains(child))
            {
                Children.Remove(child);
            }
        }

        public UIBaseElement GetFirstParent()
        {
            if(Parent != null)
            {
                return Parent.GetFirstParent();
            }

            return this;
        }

        public void NextChild()
        {
            if (Children != null && Children.Any() && (HasFocus || HasChildFocus))
            {
                if(HasFocus)
                {
                    HasFocus = false;
                    HasChildFocus = true;
                    Children[0].HasFocus = true;
                }
                else if (HasChildFocus && Children.Count > 1)
                {
                    var itemFocused = Children.First(x => x.HasFocus);
                    var index = Children.IndexOf(itemFocused);
                    itemFocused.HasFocus = false;
                    HasChildFocus = true;
                    Children[0].HasFocus = true;
                }
            }
        }

        public virtual void Dispose()
        {
            OnGainFocus = null;
            OnHoverEnd = null;
            OnHoverStart = null;
            OnLostFocus = null;
        }

        public Rectangle GetLocalBounds()
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;

            if (Parent != null)
            {
                var parentRect = Parent.GetLocalBounds();

                x = parentRect.Width * Position.X / 100 + parentRect.X;
                y = parentRect.Height * Position.Y / 100 + parentRect.Y;
                width = parentRect.Width * Width / 100;
                height = parentRect.Height * Height / 100;
            }
            else if (Layer != null)
            {
                var layerRect = Layer.GetLocalBounds();

                x = layerRect.Width * Position.X / 100 + layerRect.X;
                y = layerRect.Height * Position.Y / 100 + layerRect.Y;
                width = layerRect.Width * Width / 100;
                height = layerRect.Height * Height / 100;
            }

            return new Rectangle(x, y, width, height);
        }

    }
}
