## Install Package
```sh
dotnet add package CssInCs
```

## Add Using

Add usings to _Imports.razor file in your blazor project.

```CSharp
@using CssInCs
```

## Usage
```CSharp
// Create css object
var css = new CSSObject
{
    [".demo"] = new ()
    {
        Width = "300px",
        Height = "300px",
        Border = "1px solid #DDD",
        ["& .title"] = new ()
        {
            LineHeight = "20px",
            Color = "red"
        },
        ["& .button"] = new ()
        {
            Width = "100%",
            Height = "20px",
            TextAlign = "center",
            ["&:hover"] = new ()
            {
                Color = "blue"
            }
        }
    }
};

// Serialize the css object.
var style = css.ToString();
```

## Parent Selectors

Referencing parent selectors with & , This is the same as less. 

```csharp
var css = new CSSObject
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
```

output:

```css
a {
  color: blue;
}

a:hover {
  color: green;
}
```

The "parent selectors" operator has a variety of uses. Basically any time you need the selectors of the nested rules to be combined in other ways than the default. For example another typical use of the & is to produce repetitive class names:

```csharp
var css = new CSSObject
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
```

output:

```css
.button-ok {
  background-image: url("ok.png");
}
.button-cancel {
  background-image: url("cancel.png");
}
.button-custom {
  background-image: url("custom.png");
}
```

## Merge

You can use the merge method to inherit or merge multiple css object in some scenarios.

### Reuse global styles
```CSharp
var globalCss = new CSSObject()
{
    Background = "#EFEFEF",
    FontSize = "14px",
    Border = "1px solid #DDD",
    MarginBottom = "20px"
};

// merge with a global css
var css = new CSSObject
{
    [".div1"] = new CSSObject
    {
        Width = "100px",
        Height = "100px",
        Color = "red"
    }.Merge(globalCss),
    [".div2"] = new CSSObject
    {
        Width = "120px",
        Height = "120px",
        Color = "blue"
    }.Merge(globalCss),
    [".div3"] = new CSSObject
    {
        Width = "120px",
        Height = "120px",
        Color = "blue",
        ["& .title"] = new ()
        {
            Color = "cadetblue",
            FontSize = "20px"
        }
    }.Merge(globalCss)
};
```

### Using merge operator
```CSharp
var globalCss = new CSSObject()
{
    Background = "#EFEFEF",
    FontSize = "14px",
    Border = "1px solid #DDD",
    MarginBottom = "20px"
};

var colorCss = new CSSObject()
{
    Color = "green"
}

var css = new CSSObject
{
    [".div1"] = new CSSObject
    {
        // using "..." to merge globalCss, similar to the ...globalCss in ts.
        ["..."] = globalCss,
        Width = "100px",
        Height = "100px",
        Color = "red",
        ["..."] = colorCss, // merge operator can be used multiple times.
    }
};
```

**NOTE** : The Merge method can only overwrite the object property after instantiation, but the Merge operator can be overridden during instantiation.


### Overwrite existing styles

```CSharp
// override div3 title color
css.Merge(new CSSObject
{
    [".div3"] = new ()
    {
        ["& .title"] = new ()
        {
            Color = "yellow"
        }
    }
});
```

## Variables

You can use the variables defined in your class during css object creation, including member variables, local variables etc.

```CSharp
private string _style = "";
private string _color = "red";        // field
[Parameter]
public int Size { get; set; } = 200;  // component parameter

protected override void OnInitialized()
{
    var fontSize = "16px";            // local varibale
    var css = new CSSObject()
    {
        [".div1"] = new ()
        {
            Border = "1px solid #DDD",
            Width = $"{Size}px",
            Height = $"{Size}px",
            Color = _color,
            FontSize = fontSize,
            ["& .title"] = new ()
            {
                Color = _color,
                FontSize = fontSize
            } 
        }
    };

    // variables are effective only during initialization
    _style = css.ToString();
}
```
**Variables are effective only during initialization, you should not change the css object by modifying variables after initialization.**

### Retain CSS

```CSharp
private CSSObject _css = new ();
private string _color = "red";
[Parameter]
public int Size { get; set; } = 200;  // component parameter

protected override void OnInitialized()
{
    var fontSize = "16px";            // local varibale
    _css = new CSSObject
    {
        [".div2"] = new()
        {
            Border = "1px solid #DDD",
            Width = $"{Size}px",
            Height = $"{Size}px",
            MarginTop = "10px",
            ["& .title"] = new()
            {
                Color = _color,
                FontSize = fontSize
            }
        }
    };
}
```

If you want to change css object after initialization, you should define css object as a member variable, but we do not recommend you to do this.

The following examples shows how to change a css object.

```CSharp
private void ClickToChangeCssObject() 
{
    // 1.use merge method to change css object, @see merge example.
    _css.Merge(new CSSObject
    {
        [".div2"] = new ()
        {
            ["& .title"] = new ()
            {
                Color = "green"
            }
        }
    });

    // 2.or set property directly
    _css[".div2"]["& .title"].Color = "green";

    // rerender the style
    _style = _css.ToString();
}
```
**Remember: Never change the css object after initialization.**

## Functions

As you know, Functions are also supported.

```CSharp
private int _size = 500;
private string _style = "";

protected override async Task OnInitializedAsync()
{
    var css = new CSSObject
    {
        [".div"] = new ()
        {
            Width = $"{_size}px",
            Border = "1px solid #DDD",
            ["& .header"] = new ()
            {
                Height = "50px",
                Width = "100%",
            },
            ["& .footer"] = new ()
            {
                Height = "50px",
                Width = "100%",
            },
            ["& .body"] = new ()
            {
                // normal method
                Height = $"{CalcBodyHeight(50, 50)}px",
                Width = "100%",
                BorderTop = "1px solid #DDD",
                BorderBottom = "1px solid #DDD",
                [".container"] = new ()
                {
                    // async method
                    Height = await GetContainerSizeAsync(),
                    BackgroundColor = "#EFEFEF",
                }
            }
        }
    };
    _style = css.ToString();
}

private int CalcBodyHeight(int header, int footer)
{
    return _size - header - footer;
}

private async Task<string> GetContainerSizeAsync()
{
    return await Task.FromResult("200px");
}
```

## Animation

We designed a **Keyframe** type to create animation styles, which are supported by the AnimationName property.

Transform animation example:

```csharp
<div class="transform">
    <span class="title">Transform</span>
    <div class="animation"></div>
</div>

<Style>
    @_transform
</Style>

@code {
    private string _transform = "";

    protected override void OnInitialized()
    {
        _transform = new CSSObject
        {
            [".transform"] = new()
            {
                Width = 120,
                Height = 120,
                ["& .title"] = new ()
                {
                    Height = 20,
                    LineHeight = 20,
                    FontSize = 14
                },
                ["& .animation"] = new()
                {
                    Width = 100,
                    Height = 100,
                    BackgroundColor = "rgba(0, 0, 255, 0.5)",
                    AnimationDuration = "3s",
                    AnimationName = new Keyframe("animation-transform")
                    {
                        ["from"] = new()
                        {
                            Transform = "translateX(0px)",
                            Opacity = 1
                        },
                        ["to"] = new()
                        {
                            Transform = "translateX(100px)",
                            Opacity = 0.2f
                        }
                    }
                }
            }
        }.ToString();
    }
}
```

