namespace CssInCs
{
    public static class Styles
    {
        public static CSSObject BenchmarkCss = new()
        {
            [".pro-card"] = new()
            {
                Position = "relative",
                Display = "flex",
                FlexDirection = "column",
                BoxSizing = "border-box",
                Width = "100%",
                MarginBlock = 0,
                MarginInline = 0,
                PaddingBlock = 0,
                PaddingInline = 0,
                BackgroundColor = "#EFEFEF",
                BorderRadius = 5,
                Opacity = 0.5f,
                ["&-box-shadow"] = new()
                {
                    BoxShadow = "0 1px 2px -2px #00000029, 0 3px 6px #0000001f, 0 5px 12px 4px #00000017",
                    BorderColor = "transparent"
                },
                ["&-col"] = new()
                {
                    Width = "100%"
                },
                ["&-border"] = new()
                {
                    Border = "20px solid #DDD"
                },
                ["&-hoverable"] = new()
                {
                    Cursor = "pointer",
                    Transition = "box-shadow 0.3s, border-color 0.3s",
                    ["&:hover"] = new()
                    {
                        BorderColor = "transparent",
                        BoxShadow = "0 1px 2px -2px #00000029, 0 3px 6px #0000001f, 0 5px 12px 4px #00000017",
                    },
                    ["&.pro-card-checked:hover"] = new()
                    {
                        BorderColor = "#DDD"
                    }
                },
                ["&-checked"] = new()
                {
                    BackgroundColor = "#EFEFEF",
                    BorderColor = "#DDD",
                    ["&::after"] = new()
                    {
                        Position = "absolute",
                        InsetBlockStart = "2px",
                        InsetInlineEnd = "2px",
                        Width = 0,
                        Height = 0,
                        Border = "6px solid #485670",
                        BorderBlockEnd = "6px solid transparent",
                        BorderInlineStart = "6px solid transparent",
                        BorderStartEndRadius = "2px",
                        Content = "\"\"",
                    }
                },
                ["&:focus"] = new()
                {
                    BackgroundColor = "#EFEFEF",
                    BorderColor = "#DDD",
                },
                ["&&-size-small"] = new()
                {
                    [".pro-card"] = new()
                    {
                        ["&-header"] = new()
                        {
                            PaddingInline = "10px",
                            PaddingBlock = "10px",
                            PaddingBlockEnd = 0,
                            ["&-border"] = new()
                            {
                                PaddingBlockEnd = "10px"
                            }
                        },
                        ["&-title"] = new()
                        {
                            FontSize = "12px"
                        },
                        ["&-body"] = new()
                        {
                            PaddingInline = "10px",
                            PaddingBlock = "10px",
                        }
                    }
                }
            }
        };

        public static CSSObject Selectors_I = new ()
        {
            ["a"] = new()
            {
                Color = "blue",
                ["&:hover"] = new()
                {
                    Color = "green"
                }
            }
        };

        public static CSSObject Selectors_II = new ()
        {
            [".button"] = new()
            {
                ["&-ok"] = new()
                {
                    BackgroundImage = "url(\"ok.png\")"
                },
                ["&-cancel"] = new()
                {
                    BackgroundImage = "url(\"cancel.png\")"
                },
                ["&-custom"] = new()
                {
                    BackgroundImage = "url(\"custom.png\")"
                }
            }
        };

        public static CSSObject Selectors_III = new()
        {
            [".link"] = new()
            {
                ["& + &"] = new()
                {
                    Color = "red"
                },
                ["& &"] = new()
                {
                    Color = "green"
                },
                ["&&"] = new()
                {
                    Color = "blue"
                },
                ["&, &ish"] = new()
                {
                    Color = "cyan"
                }
            }
        };

        public static CSSObject Selectors_IV = new()
        {
            [".grand"] = new()
            {
                [".parent"] = new ()
                {
                    ["& > &"] = new ()
                    {
                        Color = "red"
                    },
                    ["& &"] = new()
                    {
                        Color = "green"
                    },
                    ["&&"] = new()
                    {
                        Color = "blue"
                    },
                    ["&, &ish"] = new()
                    {
                        Color = "cyan"
                    }
                }
            }
        };

        public static CSSObject Selectors_V = new()
        {
            [".header"] = new()
            {
                [".menu"] = new()
                {
                    BorderRadius = "5px",
                    [".no-borderradius &"] = new()
                    {
                        BackgroundImage = "url('images/button-background.png')"
                    }
                }
            }
        };
    }
}

