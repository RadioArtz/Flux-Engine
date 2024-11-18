<p align="center">
  <img src="https://github.com/user-attachments/assets/4a5b8dc4-0201-4d2f-9f31-79e4e9eaf509" alt="FluxLogoSmall" width="128" />
</p>

### FLUX ENGINE

**Flux Engine** is a **basic, x64 game engine / framework** built with **C# .NET 6**. It's a personal project for learning C# and exploring how game engines work.  
Note that Code Quality may not always be high as I experiment, temporarily hack things in and simply learn more about C#!

The engine is inspired by **Unity** and **Unreal Engine** and is designed for creating basic **2D and 3D games** or tools.  

---

## 🚀 Features  

- **Rendering**: Uses **OpenGL Core 4.1** (via OpenTK).  
- **Audio**: Supports **2D and 3D audio** using the **Bass library**.  
- **Model Loading**: Handled through **AssimpNet**.  
- **Texture Loading**: Managed with **StbImageSharp**.  
- **Cross-Platform Goals**: Primarily Windows, with Linux support in mind.  

---

## 🎮 Getting Started  

### Requirements  

1. **Visual Studio 2022**  
2. **.NET 6 SDK**  
3. [Bass library](https://www.un4seen.com/files/bass24.zip) & [.NET wrapper](https://www.un4seen.com/files/z/4/Bass24.Net.zip).  
4. NuGet dependencies (auto-managed).  

---

### 📂 Setting Up Your Project  

1. Clone this repository and open the solution in Visual Studio.  
2. Add a **new project** to the solution.  
3. Add a reference to the `FluxEngine` project.  

### 🔧 Basic Setup  

#### `Program.cs`  
```csharp
using Flux;
using Flux.Types;

namespace FluxGame
{
    public static class Game
    {
        public static void Main(string[] args)
        {
            Engine.Main(args, () => GameStart());
        }

        public static void GameStart()
        {
            Engine.window!.SetActiveScene(new ExampleScene());
        }
    }
}
```

#### Creating a Scene  
```csharp
using Flux.Types;

namespace FluxGame
{
    public class ExampleScene : FScene
    {
        public override void OnLoad()
        {
            base.OnLoad();
            // Set up your scene here
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            // Game logic goes here
        }
    }
}
```

### 🛠️ Sample Usage  
Check out the [FluxGame folder](https://github.com/RadioArtz/Flux-Engine/tree/main/FluxGame) for examples like:  
- **An osu! Mania map parser.**  
- **A primitive voxel terrain generator.**  

---

## 🤝 Contributing  

Contributions are welcome, but please keep in mind:  

- **Suggestions and changes** are preferred over feature additions since this is a learning project.  
- If you’d like to help, feel free to create an issue or pull request!  

---

## 📜 License  

This projects code is licensed under the **MIT License**.
When building with the project, make sure to check the licenses of dependencies:
**[bass](https://www.un4seen.com/)** for example, is only free for non-commercial use!


---

## ❤️ Credits  

Flux Engine wouldn’t be possible without these amazing libraries:  

- **[BulletSharp.x64](https://www.nuget.org/packages/BulletSharp.x64/)**
- **[AssimpNet](https://www.nuget.org/packages/AssimpNet/)**
- **[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)**
- **[OpenTK](https://www.nuget.org/packages/OpenTK/)**
- **[StbImageSharp](https://www.nuget.org/packages/StbImageSharp/)**  
- And many more! Check out the `packages.config` or `.csproj` for the full list.  

---

## 🔮 Future Plans  
- UI Tools
- PBR Shading with IBL and Shadowing
- RenderTextures & Post-Processing Effects  
- Transparency Rendering  
- Physics (BulletSharp.x64 integration)  
- Better Asset and Memory Management
- Level Editor

Suggestions? Let me know! 😊
