# Quick Setup Guide - New UI Features

## 🚀 Quick Start

### 1. UIThemeManager Setup (5 minutes)

1. **Create the Theme Manager:**
   - In Unity Hierarchy, right-click → Create Empty
   - Name it "UIThemeManager"
   - Add Component → Scripts → UI → UIThemeManager

2. **Assign UI Elements:**
   - In Inspector, find the UIThemeManager component
   - Drag your UI elements into the appropriate lists:
     - **Background Elements**: Main background panels
     - **Primary Elements**: Main buttons, important panels
     - **Secondary Elements**: Secondary buttons, info panels
     - **Accent Elements**: Highlight elements, special buttons
     - **Text Elements**: Main text (titles, scores)
     - **Secondary Text Elements**: Descriptions, hints

3. **Choose a Theme:**
   - In UIThemeManager Inspector, select colors or use presets
   - Play mode to see live updates
   - Call `UIThemeManager.instance.SetThemePreset()` from code to switch themes

### 2. UIAnimator Setup (2 minutes per element)

1. **Add to UI Element:**
   - Select button, panel, or any UI element
   - Add Component → Scripts → UI → UIAnimator

2. **Configure Animation:**
   - **Animation Type**: Choose effect (Scale, Fade, ScaleAndFade, Position, All)
   - **Animation Duration**: 0.3 seconds recommended
   - **Animate On Enable**: Check for automatic animation
   - **Loop Animation**: Check for continuous effects

3. **Fine-tune:**
   - Adjust Start/End Scale for size effects
   - Adjust Start/End Alpha for fade effects
   - Use Animation Curve for custom timing

### 3. EnhancedButton Setup (3 minutes per button)

1. **Add to Button:**
   - Select existing Button GameObject
   - Add Component → Scripts → UI → EnhancedButton

2. **Configure Effects:**
   - ✅ Enable Hover Effect
   - ✅ Enable Click Effect
   - ✅ Enable Ripple Effect (optional, requires ripple prefab)
   - Set Hover Scale: 1.05
   - Set Click Scale: 0.95

3. **Add Audio (optional):**
   - Assign Hover Sound clip
   - Assign Click Sound clip
   - Set Volume: 1.0

4. **Advanced:**
   - Create ripple prefab (Image with transparent white)
   - Assign Ripple Prefab for ripple effects
   - Assign Click Particle Prefab for particles on click

### 4. EnhancedScoreDisplay Setup (5 minutes)

1. **Create Score Display:**
   - In scene with GameState, find score UI
   - Add Component → Scripts → UI → EnhancedScoreDisplay

2. **Assign References:**
   - **Score Text**: Main score text component
   - **Multiplier Text**: Multiplier display text
   - **Combo Text**: Combo counter text
   - **Combo Fill Bar**: Optional progress bar image

3. **Configure Settings:**
   - Score Animation Speed: 2.0
   - Pop Scale: 1.2
   - Pop Duration: 0.2
   - Set combo thresholds and messages

4. **Integrate with Game:**
   ```csharp
   // In your game code:
   EnhancedScoreDisplay scoreDisplay = FindObjectOfType<EnhancedScoreDisplay>();
   scoreDisplay.AddScore(100);
   scoreDisplay.SetMultiplier(2);
   scoreDisplay.IncrementCombo();
   ```

### 5. ParticleEffectManager Setup (5 minutes)

1. **Create Manager:**
   - Create Empty GameObject named "ParticleEffectManager"
   - Add Component → Scripts → ParticleEffectManager

2. **Assign Prefabs:**
   - Create or assign particle effect prefabs:
     - Coin Collect Effect
     - Powerup Effect
     - Obstacle Hit Effect
     - Level Up Effect
     - Combo Effect

3. **Configure Pool:**
   - Pool Size: 20 (default, adjust based on needs)

4. **Use in Code:**
   ```csharp
   // Play effects from anywhere:
   ParticleEffectManager.instance.PlayCoinCollectEffect(transform.position);
   ParticleEffectManager.instance.PlayPowerupEffect(transform.position);
   ```

---

## 🎨 Theme Presets

### Available Themes:
1. **Modern** (Default) - Blue/Purple/Orange
2. **Sunset** - Warm orange/pink/yellow
3. **Ocean** - Cool blue/teal
4. **Forest** - Green/yellow nature
5. **Neon** - Cyberpunk high-contrast

### Switching Themes:
```csharp
// From any script:
UIThemeManager.instance.SetThemePreset(UIThemeManager.ThemePreset.Neon);
```

---

## 🎮 Integration Examples

### Example 1: Animated Menu Button
```csharp
// On your menu button GameObject:
// 1. Add EnhancedButton component
// 2. Add UIAnimator component
// 3. Configure UIAnimator:
//    - Type: ScaleAndFade
//    - Animate On Enable: true
//    - Duration: 0.3
// 4. Configure EnhancedButton:
//    - Enable all effects
//    - Assign audio clips
```

### Example 2: Score with Effects
```csharp
public class GameController : MonoBehaviour
{
    private EnhancedScoreDisplay scoreDisplay;
    
    void Start()
    {
        scoreDisplay = FindObjectOfType<EnhancedScoreDisplay>();
    }
    
    void OnCoinCollected(Vector3 position)
    {
        scoreDisplay.AddScore(10);
        scoreDisplay.IncrementCombo();
        ParticleEffectManager.instance.PlayCoinCollectEffect(position);
    }
}
```

### Example 3: Theme Selector
```csharp
public void OnThemeButtonClicked(int themeIndex)
{
    UIThemeManager.ThemePreset preset = (UIThemeManager.ThemePreset)themeIndex;
    UIThemeManager.instance.SetThemePreset(preset);
    
    // Save preference
    PlayerPrefs.SetInt("SelectedTheme", themeIndex);
}
```

---

## ⚡ Performance Tips

1. **Particle Effects:**
   - Use object pooling (already implemented)
   - Adjust pool size based on your needs
   - Disable effects not in use

2. **UI Animations:**
   - Limit simultaneous animations to 10-15
   - Use simple animation curves
   - Disable loop animations when off-screen

3. **Theme Manager:**
   - Register elements once at start
   - Avoid frequent theme switching during gameplay
   - Use cached references

---

## 🐛 Common Issues

### Issue: Animations not smooth
- **Solution**: Increase `transitionSpeed` or adjust `animationDuration`

### Issue: Button effects not working
- **Solution**: Ensure Button component exists and is enabled

### Issue: Theme colors not updating
- **Solution**: Call `ApplyTheme()` after changing colors manually

### Issue: Particles not showing
- **Solution**: Check prefab assignments and ensure ParticleSystem component exists

---

## 📱 Mobile Optimization

For mobile devices:
1. Reduce particle count in prefabs
2. Lower pool sizes (10-15 instead of 20)
3. Simplify animation curves
4. Use fewer simultaneous animations
5. Disable pulse animations if needed

---

## 🎯 Next Steps

1. ✅ Setup UIThemeManager in your main menu scene
2. ✅ Add EnhancedButton to all buttons
3. ✅ Setup EnhancedScoreDisplay in game scene
4. ✅ Create particle effect prefabs
5. ✅ Test theme switching
6. ✅ Integrate with existing game logic

---

## 📞 Need Help?

- Check the IMPROVEMENTS.md file for detailed documentation
- Review the XML comments in each script
- Test in Play mode to see effects in action
- Check Unity Console for any error messages

---

**Estimated Setup Time**: 20-30 minutes total
**Difficulty**: Beginner-friendly
**Unity Version**: 2019.3+
