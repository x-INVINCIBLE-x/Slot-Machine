# 🎰 Unity Slot Machine Game

> A clean, modular slot machine game built in Unity — designed with OOP principles, event-driven architecture, and scalable systems for an internship assignment.

---

## 📖 Game Overview

This is a fully playable slot machine game built in Unity, featuring a realistic reel-spinning experience with smooth animations, a wallet/betting system, payout evaluation, and a polished UI flow.

The player starts with a wallet balance, places a bet, starts the spin, and watches the three reels spin. If all three reels land on the same symbol, the player wins a payout based on the symbol's multiplier. The game is designed to be fair, unpredictable, and visually satisfying — all while being backed by a clean, maintainable codebase.

**Winning Symbols & Multipliers:**

| Symbol | Payout Multiplier |
|--------|------------------|
| 🍒 Cherry | 2× Bet |
| 🔔 Bell | 5× Bet |
| BAR | 10× Bet |
| 7️⃣ Seven | 20× Bet |

---

## 🚀 How to Run the WebGL Build

No Unity installation required.

### Option 1 — Play Instantly in Browser (Recommended)

Open the Unity Play link directly:

```text
Assets/Builds/Unity Play Game Link.url
```

Or open this link directly in your browser:

https://play.unity.com/en/games/54bf8065-2145-41be-8a54-ae62c35a3dc0/slot-machine

---

### Option 2 — Open Through Repository

1. Clone or download the repository:

```bash
gh repo clone x-INVINCIBLE-x/Slot-Machine
```

2. Open the following file:

```text
Assets/Builds/Unity Play Game Link.url
```

3. Copy the link from the file and paste it into your browser.
---

## ✨ Bonus Features

Beyond the core requirements, the following features were added to enhance gameplay and code quality:

### 🎮 Gameplay Additions
- **Error Feedback System** — Clear on-screen messages for invalid actions (e.g., insufficient funds, invalid bet amount) instead of silent failures.
- **Reset Flow** — A full game reset system that restores wallet and betting state cleanly, without scene reloads.

### 🏗️ Technical Additions
- **Immutable Result Structs** (`BetChangeResult`, `SpinResult`) — All system outputs are passed as value-type containers, preventing accidental mutation and making data flow explicit.
- **ReelSymbolView Mapping** — A dedicated component that bridges visual reel index and logical `SymbolType`, solving the mismatch bug where the displayed symbol and the evaluated symbol could silently diverge.
- **UI Animation Layer** (`BetUIAnimator`) — A completely separate component handles showing/hiding UI during spins, driven purely by events — zero coupling to game logic.
- **No Singletons** — All dependencies are injected via `[SerializeField]` in the Inspector, keeping systems independently testable and replaceable.

---

## 🧠 Thought Process & Approach

### 1. Requirement Analysis

The first step was a thorough reading of the assignment brief to identify what was explicitly required (winning logic, RNG, smooth animations, payouts) and what was left open-ended (architecture, bonus features, code quality). This distinction shaped every subsequent decision — requirements defined the *what*, while architecture defined the *how*.

### 2. High-Level Architecture Design

Before writing a single line of code, I mapped out the major systems the game would need:

- A **wallet** to hold currency
- A **betting system** to manage and validate bet amounts
- A **slot machine** to coordinate reel behavior
- An **RNG layer** to generate outcomes
- A **payout evaluator** to determine winnings
- A **UI layer** to reflect state to the player

Each system was treated as an independent domain with a clear boundary. This high-level map prevented scope creep during implementation and made it easy to identify where each piece of logic *should* live — before the temptation to put everything in one place could take hold.

### 3. Event-Driven Communication

A key early decision was to wire systems together through C# events rather than direct references. Systems like `SlotMachine`, `BetController`, and `PlayerWallet` emit events (`OnSpinCompleted`, `OnBetChanged`, `OnBalanceChanged`). The UI layer listens to these events passively — it never calls into gameplay logic, and gameplay logic never calls into the UI.

This eliminated the most common Unity anti-pattern: UI and gameplay tightly coupled through mutual references. The result is a UI layer that can be completely replaced or redesigned without touching a single gameplay script.

### 4. Low-Level Design & Refinement

After validating the high-level flow, I refined each system into its current low-level design:

- `BetController` owns all betting rules. `BetControls` is just the input bridge — it holds no logic.
- `PayoutEvaluator` is a pure evaluation function. It receives a `SpinResult` and returns a number. It does not touch the wallet, it does not know about UI.
- `ReelView` handles only animation. `ReelGenerator` handles only RNG. These were intentionally split because mixing visual timing with RNG logic was the source of the visual-logic mismatch bug solved by `ReelSymbolView`.
- `GameController` is the single coordinator that wires everything together — but it doesn't *own* any domain logic itself.

### 5. Emphasis on Fairness & Bug Prevention

Fairness was treated as a first-class concern. The RNG is applied at the `ReelGenerator` level before any animation begins, meaning the outcome is determined independently of visual timing. There is no possibility of the result changing mid-animation. The `ReelSymbolView` component was specifically introduced after identifying that visual reel index and logical `SymbolType` are not automatically the same — an assumption that caused subtle evaluation bugs in early testing.

### 6. Clean Code as a Goal, Not an Afterthought

Clean architecture was not retrofitted at the end — it was the constraint that guided every design choice. No god classes. No singletons. No UI-gameplay coupling. The folder structure (`Betting/`, `Core/`, `Input/`, `SlotMachine/`, `UI/`) mirrors the domain structure, so any developer reading the project for the first time can navigate it without a guide.

---

## 📁 Project Structure

```
Assets/
├── Animations/       # Animator controllers and animation clips
├── Prefabs/          # Reusable game objects (reels, buttons, popups)
├── Scripts/
│   ├── Betting/      # BetController, BetChangeResult
│   ├── Core/         # GameController, PlayerWallet
│   ├── Input/        # BetControls, BetPresetButton
│   ├── SlotMachine/  # SlotMachine, ReelView, ReelGenerator, PayoutEvaluator, etc.
│   └── UI/           # BetUI, BetConfirmationUI, PlayerWalletUI, ErrorDisplayUI, etc.
│ 
├── Builds/
    └── WebGL/        # Playable WebGL build
```

---

## 🛠️ Built With

- **Unity** (2022.x or later)
- **C#**
- **TextMeshPro** for UI text
- **Unity Animator** for reel and handle animations

---

## 📬 Submission

Developed as part of an internship assignment evaluating Unity development skills, clean code practices, and game architecture design.
