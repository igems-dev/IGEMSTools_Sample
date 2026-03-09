# IGEMS Tools (.NET 8, Windows)

This repository contains two minimal **Windows Forms** sample apps that demonstrate how to connect to IGEMS calibration devices and read live values over HID:

- **Rounder_Sample** — uses a bundled Raw HID driver (`RawHIDDriverX64.dll`) to talk to the IGEMS Rounder device.
- **Straighter_Sample** — uses `HidSharp.dll` to talk to a Straighter.

> These samples are intentionally simple: drop them into your own solution or fork this repo and adapt for your needs.

---

## Requirements

- **OS**: Windows 10/11 (x64)
- **SDK**: .NET 8 SDK
- **IDE**: Visual Studio 2022 (or newer) with Windows Forms workload
- **Hardware**: An [IGEMS Rounder](https://igems.se/cnc/rounder/) / [IGEMS Straighter](https://igems.se/cnc/straighter/)
- **Drivers/Libraries**:
  - `Rounder_Sample`: ships a native driver `RawHIDDriverX64.dll` (copied to output at build)
  - `Straighter_Sample`: references `HidSharp.dll` (see `IGEMSTools/HidSharp.dll`)

> If Windows requires driver approval for the hardware, ensure it’s installed/allowed by your IT policy.

---

## Getting Started

1. **Clone** the repo and open `IGEMSTools_Sample.sln` in Visual Studio.
2. In the **Solution Explorer**, choose the sample you want to run and set it as **Startup Project**.
3. **Build** (`Ctrl+Shift+B`) and **Run** (`F5`).

### Rounder_Sample

- Target framework: `net8.0-windows`
- Copies `RawHIDDriverX64.dll` to the output folder on build.
- Core code: `RawHID.cs` (low-level HID), `Form1.cs` (UI/logic), `Program.cs`.

Typical flow:
1. Click **Connect** to open the HID device.
2. Use **Read** / **Start** to stream samples (timer-based reader).
3. Optional: **Zero** to set current position as zero.
4. UI updates show the most recent sample value.

### Straighter_Sample

- Target framework: `net8.0-windows`
- References `HidSharp.dll` for HID communication.
- Core code: `GSensor.cs` (device abstraction), `Form1.cs` (UI/logic), `Program.cs`.

Typical flow:
1. Click **Connect** to open the LD sensor.
2. Use **Read** / **Start** to fetch periodic values.
3. UI shows latest sample and status.

---

## Project Layout

```
IGEMSTools_Sample.sln
Rounder_Sample/
  Form1.cs
  Form1.Designer.cs
  Program.cs
  RawHID.cs
  RawHIDDriverX64.dll
  RounderSample.csproj
Straighter_Sample/
  Form1.cs
  Form1.Designer.cs
  Program.cs
  GSensor.cs
  StraighterSample.csproj
  HidSharp.dll
```

> Build artifacts like `bin/`, `obj/`, and local IDE caches are ignored via `.gitignore`.

---

## Using These Samples in Your Project

- You can **copy** files/classes (e.g., `GSensor.cs`, `RawHID.cs`) directly into your app.
- Or **add as a project** and reference it.
- Both samples are **MIT-licensed**, so adapt freely (see [LICENSE](./LICENSE)).

---

## Contributions

This repo is **open source** for learning and reuse, but we don’t accept pull requests into the canonical repository.  
> Why no PRs? We keep the sample minimal and stable for documentation and support purposes.

---

## License

Released under the **MIT License**. See [LICENSE](./LICENSE).
