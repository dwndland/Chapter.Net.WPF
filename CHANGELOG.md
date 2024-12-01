# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [3.3.0] - 2024-12-01
### Added
- Added .net 9 to the supported .net versions.
### Supported .Net Versions
- .Net 8 (Windows)
- .Net 9 (Windows)

## [3.2.0] - 2024-09-20
### Added
- Added ClipboardAccessor for a more stable access to the windows clipboard for WPF.
### Supported .Net Versions
- .Net 8 (Windows)

## [3.1.0] - 2024-08-09
### Added
- Added possibility to convert a WPF Key enum to char.
### Supported .Net Versions
- .Net 8 (Windows)

## [3.0.0] - 2024-06-07
### Changed
- Update to support .Net 8 only.
### Supported .Net Versions
- .Net 8 (Windows)

## [2.0.0] - 2024-04-07
### Changed
- Extended the NotifyEventArgs from the window observer with the lParam and wParam.
### Removed
- Removed ThemeManager, that gets moved into a new library Chapter.Net.WPF.Theming.
### Supported .Net Versions
- .Net Core 3.0
- .Net Framework 4.5
- .Net 5 (Windows)
- .Net 6 (Windows)
- .Net 7 (Windows)
- .Net 8 (Windows)

## [1.1.0] - 2024-03-31
### Added
- Added more supported .net versions.
### Changed
- Use the window API hook in and out calls from the Chapter.Net.WinAPI assembly.
### Supported .Net Versions
- .Net Core 3.0
- .Net Framework 4.5
- .Net 5 (Windows)
- .Net 6 (Windows)
- .Net 7 (Windows)
- .Net 8 (Windows)

## [1.0.0] - 2023-12-23
### Added
- Init project
### Supported .Net Versions
- .Net 6
- .Net 7
- .Net 8
