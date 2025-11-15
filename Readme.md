# Endless Runner Sample Game - Enhanced Edition 🎮

<div align="center">

[![Unity Version](https://img.shields.io/badge/Unity-2019.3+-blue.svg)](https://unity3d.com/get-unity/download)
[![Documentation](https://img.shields.io/badge/docs-complete-brightgreen.svg)](QUICK_SETUP_GUIDE.md)
[![Enhancement Summary](https://img.shields.io/badge/enhancements-view%20all-orange.svg)](ENHANCEMENT_SUMMARY.md)

**📖 [Enhancement Summary](ENHANCEMENT_SUMMARY.md) • 🚀 [Quick Setup Guide](QUICK_SETUP_GUIDE.md) • 📝 [Detailed Improvements](IMPROVEMENTS.md)**

</div>

---

## 🆕 What's New in This Enhanced Version

This version includes **major improvements** over the original:

### ✨ New Features:
- 🎨 **Modern UI Theme System** - 5 beautiful theme presets with dynamic color switching
- 🎬 **Advanced Animation System** - Smooth UI animations with customizable effects
- 🎯 **Enhanced Score Display** - Animated score counter with combo system
- 💫 **Particle Effect Manager** - Professional visual effects with object pooling
- 🎮 **Enhanced Buttons** - Interactive buttons with hover, click, and ripple effects
- 🐛 **Bug Fixes** - Resolved null reference issues and improved code quality
- 📚 **Complete Documentation** - Setup guides and improvement notes

### 🔧 Technical Improvements:
- Better error handling and null safety
- Optimized performance with object pooling
- Modular, reusable UI components
- Clean, well-documented code
- Professional coding practices

---

## 🎨 Theme Presets

Choose from 5 beautiful themes:
- **Modern** - Professional blue/purple/orange (default)
- **Sunset** - Warm orange/pink/yellow tones
- **Ocean** - Cool blue/teal waters
- **Forest** - Natural green/yellow hues
- **Neon** - High-contrast cyberpunk style

Switch themes at runtime with one line of code!

---

## 🎮 Features

### Original Features:
- Endless runner gameplay
- Character selection
- Consumable power-ups
- Mission system
- Shop with in-app purchases
- Leaderboard system
- Tutorial mode

### New Enhanced Features:
- Modern animated UI with smooth transitions
- Dynamic theme switching system
- Advanced score display with combo system
- Particle effects for all interactions
- Enhanced button feedback (hover, click, ripple)
- Loading screens with progress bars
- Audio feedback for UI interactions

---

## 🚀 Getting Started

### 1. Clone the Repository

**Important: This repository uses Git Large File Support (LFS)**

```bash
# Install Git LFS
# Download from: https://git-lfs.github.com/

# Initialize Git LFS
git lfs install

# Clone the repository
git clone <repository-url>
```

### 2. Open in Unity

- Open Unity Hub
- Click "Add" and select the project folder
- Open with Unity 2019.3 or later
- Wait for initial compilation

### 3. Setup New Features (Optional)

Follow the **[Quick Setup Guide](QUICK_SETUP_GUIDE.md)** to add:
- UIThemeManager to your scenes
- EnhancedButton components to buttons
- EnhancedScoreDisplay to game UI
- ParticleEffectManager for effects

**Estimated setup time: 20-30 minutes**

---

## 🎯 Building the Game

### Before Building:

1. **Build Addressable Assets:**
   - Open: Window > Asset Management > Addressables > Groups
   - Click: Build > New Build > Default Build Script
   - Wait for build to complete

2. **Configure Build Settings:**
   - File > Build Settings
   - Select your target platform
   - Add required scenes

3. **Build:**
   - Click "Build" or "Build and Run"
   - Select output folder

### Platform Notes:
- **Mobile**: All new features are mobile-optimized
- **Desktop**: Full feature support
- **WebGL**: Supported with standard limitations

---

## 📖 Project Structure

```
Assets/
├── Scripts/
│   ├── Characters/          # Character controllers and data
│   ├── GameManager/         # Game state management
│   ├── Tracks/              # Track generation system
│   ├── UI/                  # UI components (Enhanced!)
│   │   ├── UIThemeManager.cs      [NEW]
│   │   ├── UIAnimator.cs          [NEW]
│   │   ├── EnhancedButton.cs      [NEW]
│   │   ├── EnhancedScoreDisplay.cs [NEW]
│   │   └── MainMenu.cs            [IMPROVED]
│   ├── ParticleEffectManager.cs   [NEW]
│   └── ...
├── Scenes/               # Game scenes
├── Prefabs/              # Reusable game objects
├── Materials/            # Materials and textures
└── ...
```

---

## 🎨 Customization

### Change UI Theme:
```csharp
// In any script
UIThemeManager.instance.SetThemePreset(UIThemeManager.ThemePreset.Ocean);
```

### Add Button Animation:
```csharp
// Add to button GameObject
UIAnimator animator = gameObject.AddComponent<UIAnimator>();
animator.animationType = UIAnimator.AnimationType.ScaleAndFade;
animator.animateOnEnable = true;
```

### Play Particle Effects:
```csharp
// From anywhere in code
ParticleEffectManager.instance.PlayCoinCollectEffect(position);
```

### Update Score Display:
```csharp
EnhancedScoreDisplay score = FindObjectOfType<EnhancedScoreDisplay>();
score.AddScore(100);
score.SetMultiplier(2);
score.IncrementCombo();
```

---

## 🛠️ Development

### Requirements:
- Unity 2019.3 or later
- Git LFS installed
- Basic C# knowledge

### Recommended Setup:
- Visual Studio 2019+ or VS Code
- Unity Asset Management window
- Scene view visible for testing

---

## 📝 Services & Integration

### Unity Services:
By default, all services are disabled. To enable:

1. Switch to mobile platform (for Ads)
2. Enable services in Unity Services window
3. Follow original INSTRUCTIONS.txt for detailed setup

### In-App Purchases:
- Import UnityPurchasing package
- Configure products in shop
- Test with fake store in editor

---

## 🎓 Learning Resources

This project demonstrates:
- Modern Unity UI development
- Game state management
- Object pooling for performance
- Addressable assets system
- Mobile game optimization
- Clean code architecture

### Code Quality:
- ✅ Null safety checks
- ✅ Error handling
- ✅ XML documentation
- ✅ Consistent naming
- ✅ Best practices

---

## 🐛 Troubleshooting

### Common Issues:

**Compilation Errors:**
- Ensure all packages are imported
- Check Unity version compatibility
- Rebuild addressables

**Missing Assets:**
- Verify Git LFS is installed
- Re-pull repository with LFS
- Check addressables are built

**UI Not Themed:**
- Add UIThemeManager to scene
- Assign UI elements to lists
- Call ApplyTheme()

**Animations Not Playing:**
- Check UIAnimator component exists
- Verify animateOnEnable is checked
- Play mode to test

More solutions in **[Quick Setup Guide](QUICK_SETUP_GUIDE.md)**

---

## 📊 Performance

### Optimizations:
- ✅ Object pooling for particles
- ✅ Efficient UI updates
- ✅ Cached references
- ✅ Minimal GC allocations
- ✅ Mobile-friendly effects

### Tested On:
- Windows Desktop
- Android devices
- iOS devices (original version)

---

## 🤝 Contributing

### Original Repository:
This is an enhanced version of Unity's official Endless Runner sample.

### Improvements Made:
- Code quality and bug fixes
- Modern UI system
- Advanced animations
- Visual effects system
- Comprehensive documentation

---

## 📄 License

This project uses Unity's sample game as a base.
New enhancements and features are provided as-is for learning purposes.

---

## 🎉 Getting Help

1. **Check Documentation:**
   - [Enhancement Summary](ENHANCEMENT_SUMMARY.md)
   - [Quick Setup Guide](QUICK_SETUP_GUIDE.md)
   - [Detailed Improvements](IMPROVEMENTS.md)

2. **Common Issues:**
   - See Troubleshooting section above
   - Check Unity Console for errors
   - Verify component assignments

3. **Original Resources:**
   - [Unity Learn Tutorial](https://unity3d.com/learn/tutorials/topics/mobile-touch/trash-dash-code-walkthrough)
   - [Original Asset Store Page](https://assetstore.unity.com/packages/essentials/tutorial-projects/endless-runner-sample-game-87901)

---

## ⭐ What Makes This Version Special

### Original Version:
- Basic endless runner
- Simple UI
- Core gameplay features

### Enhanced Version:
- ✨ Modern, animated UI
- 🎨 Multiple theme options
- 💫 Professional effects
- 🐛 Bug fixes and improvements
- 📚 Complete documentation
- 🚀 Easy to customize
- 🎯 Production-ready code

---

## 📈 Version History

### v2.0 - Enhanced Edition (Current)
- Added UIThemeManager with 5 presets
- Created UIAnimator system
- Implemented EnhancedScoreDisplay with combos
- Added ParticleEffectManager
- Created EnhancedButton component
- Fixed multiple bugs
- Added comprehensive documentation

### v1.0 - Original Version
- Base endless runner game
- Tutorial system
- Addressable assets
- Lightweight rendering pipeline

---

## 🎯 Next Steps

1. ✅ Clone the repository
2. ✅ Open in Unity 2019.3+
3. ✅ Read the Quick Setup Guide
4. ✅ Build addressable assets
5. ✅ Test the game
6. ✅ Add new UI features
7. ✅ Customize themes
8. ✅ Build and enjoy!

---

**Made with ❤️ using Unity**

**Original by**: Unity Technologies  
**Enhanced by**: KINSHUK SAXENA
**Version**: 2.0 - Enhanced Edition  
**Date**: November 2025

🎮 **Happy Gaming!** 🎮
