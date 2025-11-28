# CLAUDE.md - Little Adventure Project Guide

## Project Overview

**Little Adventure** is a Unity 6 game project built with the Universal Render Pipeline (URP). This is an action-adventure game featuring character movement, combat, and interaction systems.

### Key Technologies
- **Engine**: Unity 6000.3.0b9 (Unity 6 Beta)
- **Render Pipeline**: Universal Render Pipeline (URP) 17.3.0
- **Input System**: Unity Input System 1.15.0
- **Version Control**: Git with Git LFS for large binary files
- **IDE Support**: JetBrains Rider, Visual Studio

## Repository Structure

```
LittleAdventure/
├── Assets/
│   ├── InputSystem_Actions.inputactions    # Input action mappings
│   ├── Scenes/                             # Unity scenes
│   │   └── SampleScene.unity               # Default sample scene
│   ├── Settings/                           # Render pipeline settings
│   │   ├── PC_RPAsset.asset               # PC render pipeline config
│   │   ├── PC_Renderer.asset              # PC renderer config
│   │   ├── Mobile_RPAsset.asset           # Mobile render pipeline config
│   │   ├── Mobile_Renderer.asset          # Mobile renderer config
│   │   └── DefaultVolumeProfile.asset     # Post-processing volume
│   ├── TutorialInfo/                       # Unity template tutorial files
│   └── ProjectLittleAdventurer/            # Main project folder
│       ├── Mesh Asset/                     # 3D models and textures
│       │   ├── Character/                  # Character models
│       │   │   ├── MainCharacter/          # Player character (Andie)
│       │   │   ├── NPC_01/                 # Non-player character 1
│       │   │   ├── NPC_02/                 # Non-player character 2
│       │   │   └── Sword/                  # Weapon models
│       │   ├── Environment/                # Environment assets
│       │   └── MainMenu/                   # Main menu assets
│       ├── Prefab/                         # Prefabricated game objects
│       │   ├── MainMenu/                   # Main menu prefabs
│       │   └── UI/                         # UI prefabs
│       ├── Settings/                       # Project-specific settings
│       │   └── BehindTheWall/             # Rendering settings
│       ├── Tool/                           # Utility scripts
│       ├── UI/                             # UI assets
│       ├── VFX/                            # Visual effects
│       │   ├── Materials/                  # VFX materials
│       │   ├── Mesh/                       # VFX meshes
│       │   ├── Prefab/                     # VFX prefabs
│       │   ├── Shaders/                    # VFX shaders
│       │   ├── Textures/                   # VFX textures
│       │   └── VFX/                        # Visual effect graphs
│       ├── PP/                             # Post-processing effects
│       └── Overview.unity                  # Main overview scene
├── Packages/                               # Unity package dependencies
├── ProjectSettings/                        # Unity project configuration
└── .git/                                   # Git version control
```

## Asset Organization Conventions

### Character Assets
Each character folder contains:
- **FBX file**: 3D model with animations
- **Material files**:
  - `[CharacterName].mat` - Main in-game material
  - `[CharacterName] MainMenu.mat` - Menu-specific material
- **Texture subfolder**:
  - `Base.png` - Base color/albedo map
  - `Normal.png` - Normal map for surface detail
  - `AO.png` - Ambient occlusion map
  - `MetallicSmoothness.png` - Metallic and smoothness combined
  - `Emission.png` or `Emssion.png` - Emissive map

### VFX Assets
Visual effects are organized into:
- **Materials**: Shader-based materials for effects
- **Mesh**: Geometry for particle effects
- **Prefab**: Complete effect assemblies
- **Shaders**: Custom shader graphs
- **Textures**: Effect textures and sprite sheets
- **VFX**: Visual Effect Graph assets

## Code Conventions

### Naming Conventions

1. **Class Prefixes**:
   - `G_` prefix for game-specific utility classes (e.g., `G_CullManager`, `G_Culler`)
   - Standard Unity naming for MonoBehaviour components

2. **Field Naming**:
   - **Private fields**: Start with underscore `_fieldName`
   - **Public fields**: PascalCase `FieldName`
   - **Properties**: PascalCase `PropertyName`

3. **Method Naming**:
   - PascalCase for all methods: `MethodName()`
   - Unity event methods follow Unity conventions: `Start()`, `Awake()`, `Update()`

### Code Style Examples

```csharp
public class G_ExampleManager : MonoBehaviour
{
    // Singleton pattern - common in manager classes
    public static G_ExampleManager Instance;

    // Public fields - visible in Inspector
    public float PublicValue = 10f;

    // Private fields - internal state
    private Renderer _renderer;
    private List<GameObject> _objects;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        // Implementation
    }

    public void PublicMethod()
    {
        // Public API
    }
}
```

### Existing Systems

#### Culling System
Located in `Assets/ProjectLittleAdventurer/Tool/`:
- **G_CullManager**: Manages visibility culling using Unity's CullingGroup API
  - Handles both mesh renderers and particle systems
  - Uses bounding spheres for efficient culling
  - Configurable culling radii for different object types
- **G_Culler**: Component attached to objects that should be culled
  - Automatically detects object type (MeshRenderer or ParticleSystem)
  - Handles enabling/disabling based on visibility

## Input System

The project uses Unity's new Input System with action-based input.

### Input Action Maps

**Player Actions** (defined in `InputSystem_Actions.inputactions`):
- **Move**: WASD/Arrow keys, gamepad left stick
- **Look**: Mouse delta, gamepad right stick
- **Attack**: Mouse left button, gamepad west button
- **Jump**: Space, gamepad south button
- **Sprint**: Left Shift, gamepad left stick press
- **Crouch**: C key, gamepad east button
- **Interact**: E key (hold), gamepad north button
- **Previous/Next**: 1/2 keys, gamepad d-pad

**UI Actions**:
- Standard UI navigation bindings
- Multi-platform support (Keyboard/Mouse, Gamepad, Touch, XR)

### Supported Control Schemes
1. Keyboard & Mouse
2. Gamepad
3. Touch (Mobile)
4. Joystick
5. XR (Virtual Reality)

## Git Workflow

### Branch Naming Convention
- Feature branches: `claude/claude-md-miiymjgnncwovwiq-[SessionID]`
- Work should be done on feature branches, not directly on main

### Commit Message Convention
The project follows a section-based commit message format:
```
S[Section]-[Number]: Brief description of changes

Examples:
- S01-07: Importing Game Asset Package
- Initial Unity 6 URP Little Adventure project setup
```

### Git LFS Configuration
The following file types are tracked with Git LFS:
- **3D Models**: `.psd`, `.fbx`, `.obj`, `.blend`
- **Textures**: `.png`, `.jpg`, `.jpeg`, `.tga`, `.tif`, `.exr`, `.hdr`
- **Audio**: `.mp3`, `.wav`, `.ogg`
- **Video**: `.mp4`, `.mov`
- **Unity Files**: `.asset`, `.unity`, `.unitypackage`

### Important Files NOT in Version Control
Per `.gitignore`:
- `/Library/` - Unity's cached assets
- `/Temp/` - Temporary build files
- `/Obj/`, `/Build/`, `/Builds/` - Build outputs
- `/Logs/` - Unity log files
- `/UserSettings/` - User-specific settings
- `.vs/`, `.gradle/`, `.idea/` (partial) - IDE files
- Generated solution/project files (`.csproj`, `.sln`)

## Unity Project Configuration

### Render Pipeline
The project maintains separate render pipeline assets for different platforms:

**PC Configuration**:
- Asset: `Assets/Settings/PC_RPAsset.asset`
- Renderer: `Assets/Settings/PC_Renderer.asset`
- Optimized for desktop performance

**Mobile Configuration**:
- Asset: `Assets/Settings/Mobile_RPAsset.asset`
- Renderer: `Assets/Settings/Mobile_Renderer.asset`
- Optimized for mobile performance

### Post-Processing
- Default Volume Profile: `Assets/Settings/DefaultVolumeProfile.asset`
- Scene-specific profiles available in scene folders

### Unity Packages
Key packages (from `Packages/manifest.json`):
- `com.unity.render-pipelines.universal`: 17.3.0
- `com.unity.inputsystem`: 1.15.0
- `com.unity.ai.navigation`: 2.0.9
- `com.unity.timeline`: 1.8.9
- `com.unity.visualscripting`: 1.9.9
- `com.unity.test-framework`: 1.6.0

## Development Guidelines for AI Assistants

### When Adding New Features

1. **Understand the Context First**:
   - Review existing scripts in the relevant area
   - Check for similar functionality that already exists
   - Understand the naming conventions and patterns in use

2. **Follow Existing Patterns**:
   - Use the `G_` prefix for game utility classes
   - Follow the singleton pattern for manager classes where appropriate
   - Use underscore prefix for private fields
   - Match the code organization style

3. **Asset Management**:
   - Place character assets in `Assets/ProjectLittleAdventurer/Mesh Asset/Character/`
   - Place environment assets in `Assets/ProjectLittleAdventurer/Mesh Asset/Environment/`
   - Create prefabs in appropriate `Prefab/` subfolders
   - Keep VFX organized in the `VFX/` structure
   - Ensure all binary files are added to `.gitattributes` for LFS if needed

4. **Scene Management**:
   - Main project scene: `Assets/ProjectLittleAdventurer/Overview.unity`
   - Keep test/sample scenes in `Assets/Scenes/`

5. **Input System**:
   - Modify `Assets/InputSystem_Actions.inputactions` for new input actions
   - Regenerate input action C# class after modifications
   - Support multiple control schemes where applicable

6. **Performance Considerations**:
   - Use the culling system for objects that don't need constant rendering
   - Consider both PC and mobile performance targets
   - Utilize object pooling for frequently instantiated objects

### When Modifying Existing Code

1. **Before Making Changes**:
   - Read the entire file to understand its purpose
   - Check for dependencies (scripts that reference this code)
   - Look for similar patterns in other files

2. **Maintain Consistency**:
   - Match existing formatting and style
   - Keep naming conventions consistent
   - Preserve design patterns in use

3. **Testing**:
   - Test in both the Unity Editor and built player when possible
   - Consider both PC and mobile configurations
   - Verify input works across different control schemes

### Common Pitfalls to Avoid

1. **Don't** commit Unity's generated files (Library, Temp, Obj, etc.)
2. **Don't** break the singleton pattern if a class uses it
3. **Don't** ignore the platform-specific render pipeline configurations
4. **Don't** add binary assets without ensuring they're in `.gitattributes`
5. **Don't** create scripts outside of appropriate folders
6. **Don't** modify the Input System asset without regenerating the C# class

### Working with Unity Scenes

Unity scene files (`.unity`) and assets (`.asset`) are binary files tracked by Git LFS. When making scene changes:
1. Keep changes minimal and focused
2. Avoid unnecessary reformatting
3. Test thoroughly before committing
4. Note that scene merge conflicts are difficult to resolve manually

### Debugging Tips

1. **Culling Issues**: Check `G_CullManager` settings for radius values
2. **Input Not Working**: Verify Input System package is enabled in Player Settings
3. **Render Pipeline Issues**: Ensure correct render pipeline asset is selected in Graphics Settings
4. **Performance Problems**: Check if culling system is properly configured

## Build Settings

### Target Platforms
The project is configured to support:
- **Primary**: PC (Windows, macOS, Linux)
- **Secondary**: Mobile (Android, iOS) with optimized settings

### Quality Settings
Multiple quality levels are defined in `ProjectSettings/QualitySettings.asset`:
- Configure appropriate quality level for target platform
- Mobile uses lower quality settings for performance

## Additional Resources

### Learning Resources
- Unity Documentation: https://docs.unity3d.com/
- URP Documentation: https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest
- Input System Documentation: https://docs.unity3d.com/Packages/com.unity.inputsystem@latest

### Project-Specific Documentation
- Check `Assets/TutorialInfo/` for Unity template information
- Review commit history for understanding project evolution: `git log --oneline`

## Quick Reference

### Essential File Paths
```
Input Actions: Assets/InputSystem_Actions.inputactions
Main Scene: Assets/ProjectLittleAdventurer/Overview.unity
Scripts: Assets/ProjectLittleAdventurer/Tool/*.cs
Prefabs: Assets/ProjectLittleAdventurer/Prefab/
Settings: Assets/Settings/ and ProjectSettings/
```

### Common Commands
```bash
# View project status
git status

# View commit history with file changes
git log --stat

# Check Unity version
cat ProjectSettings/ProjectVersion.txt

# List all C# scripts
find Assets -name "*.cs" -type f

# Find all prefabs
find Assets -name "*.prefab" -type f
```

## Project Status

**Current Phase**: Early development - asset import and initial setup complete

**Recent Commits**:
- S01-07: Importing Game Asset Package
- Initial Unity 6 URP Little Adventure project setup
- Initial commit: Git LFS and Unity gitignore setup

**Main Scene**: `Assets/ProjectLittleAdventurer/Overview.unity`

---

**Last Updated**: 2025-11-28
**Unity Version**: 6000.3.0b9
**URP Version**: 17.3.0

For questions or clarifications about this project, review the commit history and existing code patterns before making changes.
