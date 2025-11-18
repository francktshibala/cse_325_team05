# Clinic Queue System - Design System Guide

**Version:** 1.0
**Last Updated:** November 2024
**Theme:** Modern Medical Blue

---

## Purpose

This design system ensures consistent styling across all features and components in the Clinic Queue & Appointments System. All team members MUST follow these guidelines when implementing new features.

---

## Color Palette

### Primary Colors

```css
--primary-blue: #2C5F8D;        /* Main brand color - buttons, headers */
--primary-blue-dark: #1e4566;   /* Hover states, emphasis */
--primary-blue-light: #4a7ba7;  /* Backgrounds, subtle elements */
--primary-blue-lighter: #e3eef7; /* Very light backgrounds */
```

### Secondary Colors

```css
--secondary-teal: #17a2b8;      /* Secondary actions, accents */
--secondary-teal-dark: #117a8b; /* Hover states */
--secondary-teal-light: #d1ecf1; /* Backgrounds, alerts */
```

### Accent Colors

```css
--accent-green: #28a745;        /* Success states, positive actions */
--accent-green-dark: #1e7e34;   /* Hover states */
--accent-green-light: #d4edda;  /* Success backgrounds */

--accent-orange: #fd7e14;       /* Warnings, important notices */
--accent-orange-dark: #dc6502;  /* Hover states */
--accent-orange-light: #fff3cd; /* Warning backgrounds */

--accent-red: #dc3545;          /* Errors, destructive actions */
--accent-red-dark: #bd2130;     /* Hover states */
--accent-red-light: #f8d7da;    /* Error backgrounds */
```

### Neutral Colors

```css
--white: #ffffff;
--gray-50: #f8f9fa;             /* Very light backgrounds */
--gray-100: #f5f7f9;            /* Light backgrounds */
--gray-200: #e9ecef;            /* Borders, dividers */
--gray-300: #dee2e6;            /* Disabled states */
--gray-400: #ced4da;            /* Placeholders */
--gray-500: #adb5bd;            /* Secondary text */
--gray-600: #6c757d;            /* Muted text */
--gray-700: #495057;            /* Body text */
--gray-800: #343a40;            /* Dark text */
--gray-900: #212529;            /* Headings, emphasis */
--black: #000000;
```

### Healthcare-Specific Colors

```css
--queue-waiting: #ffc107;       /* Queue status: Waiting */
--queue-called: #17a2b8;        /* Queue status: Called */
--queue-inroom: #28a745;        /* Queue status: In Room */
--appointment-scheduled: #2C5F8D; /* Appointment: Scheduled */
--appointment-cancelled: #dc3545; /* Appointment: Cancelled */
--appointment-completed: #6c757d; /* Appointment: Completed */
```

---

## Typography

### Font Family

```css
--font-family-base: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto,
                    "Helvetica Neue", Arial, sans-serif;
--font-family-monospace: SFMono-Regular, Menlo, Monaco, Consolas,
                         "Liberation Mono", "Courier New", monospace;
```

### Font Sizes

```css
--font-size-xs: 0.75rem;     /* 12px - Small labels, metadata */
--font-size-sm: 0.875rem;    /* 14px - Secondary text */
--font-size-base: 1rem;      /* 16px - Body text */
--font-size-lg: 1.125rem;    /* 18px - Large body text */
--font-size-xl: 1.25rem;     /* 20px - Subheadings */
--font-size-2xl: 1.5rem;     /* 24px - Section headers */
--font-size-3xl: 1.875rem;   /* 30px - Page titles */
--font-size-4xl: 2.25rem;    /* 36px - Hero headings */
```

### Font Weights

```css
--font-weight-light: 300;
--font-weight-normal: 400;
--font-weight-medium: 500;
--font-weight-semibold: 600;
--font-weight-bold: 700;
```

### Line Heights

```css
--line-height-tight: 1.25;   /* Headings */
--line-height-normal: 1.5;   /* Body text */
--line-height-relaxed: 1.75; /* Large text blocks */
```

### Text Styles

#### Headings
```css
h1 { font-size: var(--font-size-4xl); font-weight: var(--font-weight-bold); color: var(--gray-900); }
h2 { font-size: var(--font-size-3xl); font-weight: var(--font-weight-bold); color: var(--gray-900); }
h3 { font-size: var(--font-size-2xl); font-weight: var(--font-weight-semibold); color: var(--gray-800); }
h4 { font-size: var(--font-size-xl); font-weight: var(--font-weight-semibold); color: var(--gray-800); }
h5 { font-size: var(--font-size-lg); font-weight: var(--font-weight-medium); color: var(--gray-700); }
```

#### Body Text
```css
p { font-size: var(--font-size-base); line-height: var(--line-height-normal); color: var(--gray-700); }
.text-muted { color: var(--gray-600); }
.text-small { font-size: var(--font-size-sm); }
```

---

## Spacing System

Use consistent spacing multiples of 4px or 8px:

```css
--space-1: 0.25rem;  /* 4px */
--space-2: 0.5rem;   /* 8px */
--space-3: 0.75rem;  /* 12px */
--space-4: 1rem;     /* 16px */
--space-5: 1.25rem;  /* 20px */
--space-6: 1.5rem;   /* 24px */
--space-8: 2rem;     /* 32px */
--space-10: 2.5rem;  /* 40px */
--space-12: 3rem;    /* 48px */
--space-16: 4rem;    /* 64px */
--space-20: 5rem;    /* 80px */
```

### Common Spacing Patterns

- **Component padding:** 16px (--space-4)
- **Card padding:** 24px (--space-6)
- **Section spacing:** 32px (--space-8)
- **Page margins:** 24px (--space-6)
- **Button padding:** 8px 16px
- **Input padding:** 12px 16px

---

## Buttons

### Primary Button (Main Actions)

```css
.btn-primary {
    background-color: var(--primary-blue);
    color: var(--white);
    border: none;
    padding: 12px 24px;
    border-radius: 6px;
    font-weight: var(--font-weight-medium);
    transition: all 0.2s ease;
}

.btn-primary:hover {
    background-color: var(--primary-blue-dark);
    transform: translateY(-1px);
    box-shadow: 0 4px 12px rgba(44, 95, 141, 0.3);
}
```

### Secondary Button

```css
.btn-secondary {
    background-color: var(--secondary-teal);
    color: var(--white);
    /* Same structure as primary */
}
```

### Outline Button

```css
.btn-outline-primary {
    background-color: transparent;
    color: var(--primary-blue);
    border: 2px solid var(--primary-blue);
}

.btn-outline-primary:hover {
    background-color: var(--primary-blue);
    color: var(--white);
}
```

### Button Sizes

```css
.btn-sm { padding: 8px 16px; font-size: var(--font-size-sm); }
.btn-md { padding: 12px 24px; font-size: var(--font-size-base); }
.btn-lg { padding: 16px 32px; font-size: var(--font-size-lg); }
```

### Button States

```css
.btn:disabled {
    opacity: 0.6;
    cursor: not-allowed;
}

.btn-success { background-color: var(--accent-green); }
.btn-warning { background-color: var(--accent-orange); }
.btn-danger { background-color: var(--accent-red); }
```

---

## Cards

### Standard Card

```css
.card {
    background: var(--white);
    border: 1px solid var(--gray-200);
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
    padding: 24px;
    transition: all 0.3s ease;
}

.card:hover {
    box-shadow: 0 4px 16px rgba(0, 0, 0, 0.12);
    transform: translateY(-2px);
}
```

### Card Header

```css
.card-header {
    background-color: var(--gray-50);
    border-bottom: 2px solid var(--primary-blue);
    padding: 16px 24px;
    border-radius: 8px 8px 0 0;
}

.card-title {
    font-size: var(--font-size-xl);
    font-weight: var(--font-weight-semibold);
    color: var(--gray-900);
    margin: 0;
}
```

### Card Body

```css
.card-body {
    padding: 24px;
}
```

### Card Footer

```css
.card-footer {
    background-color: var(--gray-50);
    border-top: 1px solid var(--gray-200);
    padding: 16px 24px;
    border-radius: 0 0 8px 8px;
}
```

---

## Forms

### Input Fields

```css
.form-control {
    border: 2px solid var(--gray-300);
    border-radius: 6px;
    padding: 12px 16px;
    font-size: var(--font-size-base);
    transition: all 0.2s ease;
}

.form-control:focus {
    border-color: var(--primary-blue);
    outline: none;
    box-shadow: 0 0 0 3px rgba(44, 95, 141, 0.1);
}

.form-control:disabled {
    background-color: var(--gray-100);
    cursor: not-allowed;
}
```

### Form Labels

```css
.form-label {
    font-size: var(--font-size-sm);
    font-weight: var(--font-weight-medium);
    color: var(--gray-700);
    margin-bottom: 8px;
    display: block;
}
```

### Form Groups

```css
.form-group {
    margin-bottom: 24px;
}
```

### Validation States

```css
.form-control.is-valid {
    border-color: var(--accent-green);
}

.form-control.is-invalid {
    border-color: var(--accent-red);
}

.invalid-feedback {
    color: var(--accent-red);
    font-size: var(--font-size-sm);
    margin-top: 4px;
}

.valid-feedback {
    color: var(--accent-green);
    font-size: var(--font-size-sm);
    margin-top: 4px;
}
```

---

## Navigation

### Top Bar

```css
.top-row {
    background-color: var(--primary-blue);
    color: var(--white);
    height: 60px;
    padding: 0 24px;
    display: flex;
    align-items: center;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}
```

### Sidebar

```css
.sidebar {
    background-color: #2c3e50;
    color: var(--white);
    width: 260px;
    min-height: 100vh;
}
```

### Nav Links

```css
.nav-link {
    color: rgba(255, 255, 255, 0.8);
    padding: 12px 20px;
    transition: all 0.2s ease;
    border-left: 3px solid transparent;
}

.nav-link:hover {
    background-color: rgba(255, 255, 255, 0.1);
    color: var(--white);
    border-left-color: var(--secondary-teal);
}

.nav-link.active {
    background-color: rgba(23, 162, 184, 0.2);
    color: var(--white);
    border-left-color: var(--secondary-teal);
    font-weight: var(--font-weight-medium);
}
```

---

## Tables

### Standard Table

```css
.table {
    width: 100%;
    border-collapse: collapse;
}

.table thead {
    background-color: var(--gray-50);
    border-bottom: 2px solid var(--primary-blue);
}

.table th {
    padding: 16px;
    text-align: left;
    font-weight: var(--font-weight-semibold);
    color: var(--gray-800);
    font-size: var(--font-size-sm);
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

.table td {
    padding: 16px;
    border-bottom: 1px solid var(--gray-200);
    color: var(--gray-700);
}

.table tbody tr:hover {
    background-color: var(--gray-50);
}
```

---

## Badges & Status Indicators

### Status Badges

```css
.badge {
    display: inline-block;
    padding: 4px 12px;
    border-radius: 12px;
    font-size: var(--font-size-xs);
    font-weight: var(--font-weight-semibold);
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

.badge-waiting { background-color: var(--queue-waiting); color: var(--gray-900); }
.badge-called { background-color: var(--queue-called); color: var(--white); }
.badge-inroom { background-color: var(--queue-inroom); color: var(--white); }
.badge-completed { background-color: var(--gray-600); color: var(--white); }
.badge-cancelled { background-color: var(--accent-red); color: var(--white); }
```

---

## Alerts & Notifications

### Alert Boxes

```css
.alert {
    padding: 16px 20px;
    border-radius: 6px;
    border-left: 4px solid;
    margin-bottom: 16px;
}

.alert-info {
    background-color: var(--secondary-teal-light);
    border-color: var(--secondary-teal);
    color: #0c5460;
}

.alert-success {
    background-color: var(--accent-green-light);
    border-color: var(--accent-green);
    color: #155724;
}

.alert-warning {
    background-color: var(--accent-orange-light);
    border-color: var(--accent-orange);
    color: #856404;
}

.alert-danger {
    background-color: var(--accent-red-light);
    border-color: var(--accent-red);
    color: #721c24;
}
```

---

## Shadows & Elevation

```css
--shadow-sm: 0 1px 3px rgba(0, 0, 0, 0.08);
--shadow-md: 0 2px 8px rgba(0, 0, 0, 0.1);
--shadow-lg: 0 4px 16px rgba(0, 0, 0, 0.12);
--shadow-xl: 0 8px 24px rgba(0, 0, 0, 0.15);
```

### Usage

- **Cards:** shadow-md
- **Modals:** shadow-xl
- **Dropdowns:** shadow-lg
- **Hover states:** shadow-lg

---

## Border Radius

```css
--radius-sm: 4px;    /* Small elements, badges */
--radius-md: 6px;    /* Buttons, inputs */
--radius-lg: 8px;    /* Cards, containers */
--radius-xl: 12px;   /* Large containers */
--radius-full: 9999px; /* Circular elements */
```

---

## Icons

Use Bootstrap Icons consistently:

### Icon Sizes

```css
.icon-sm { font-size: 16px; }
.icon-md { font-size: 20px; }
.icon-lg { font-size: 24px; }
.icon-xl { font-size: 32px; }
```

### Icon Colors

- **Primary actions:** var(--primary-blue)
- **Success:** var(--accent-green)
- **Warning:** var(--accent-orange)
- **Danger:** var(--accent-red)
- **Info:** var(--secondary-teal)

---

## Responsive Breakpoints

```css
/* Mobile First Approach */
--breakpoint-sm: 576px;   /* Small devices */
--breakpoint-md: 768px;   /* Tablets */
--breakpoint-lg: 992px;   /* Desktops */
--breakpoint-xl: 1200px;  /* Large desktops */
--breakpoint-xxl: 1400px; /* Extra large */
```

### Usage Example

```css
/* Mobile */
.container { padding: 16px; }

/* Tablet and up */
@media (min-width: 768px) {
    .container { padding: 24px; }
}

/* Desktop and up */
@media (min-width: 992px) {
    .container { padding: 32px; }
}
```

---

## Component-Specific Patterns

### Patient Queue Cards

```css
.queue-card {
    border-left: 4px solid var(--queue-waiting);
    /* Changes based on status */
}

.queue-card.waiting { border-left-color: var(--queue-waiting); }
.queue-card.called { border-left-color: var(--queue-called); }
.queue-card.inroom { border-left-color: var(--queue-inroom); }
```

### Appointment Cards

```css
.appointment-card {
    background: linear-gradient(135deg, var(--white) 0%, var(--gray-50) 100%);
    border: 2px solid var(--gray-200);
}

.appointment-card.upcoming {
    border-color: var(--primary-blue);
    background: linear-gradient(135deg, var(--white) 0%, var(--primary-blue-lighter) 100%);
}
```

### Provider Schedules

```css
.schedule-slot {
    border: 2px dashed var(--gray-300);
    border-radius: 6px;
    padding: 12px;
}

.schedule-slot.available {
    background-color: var(--accent-green-light);
    border-color: var(--accent-green);
}

.schedule-slot.booked {
    background-color: var(--gray-100);
    border-style: solid;
}
```

---

## Accessibility Guidelines

### Contrast Ratios

- **Normal text:** Minimum 4.5:1
- **Large text (18px+):** Minimum 3:1
- **UI components:** Minimum 3:1

All colors in this design system meet WCAG 2.1 AA standards.

### Focus States

Always provide visible focus indicators:

```css
*:focus {
    outline: 2px solid var(--primary-blue);
    outline-offset: 2px;
}

button:focus,
.btn:focus {
    box-shadow: 0 0 0 3px rgba(44, 95, 141, 0.3);
}
```

### Keyboard Navigation

- All interactive elements must be keyboard accessible
- Tab order should be logical
- Skip navigation links for screen readers

---

## Animation & Transitions

### Standard Transitions

```css
--transition-fast: 0.15s ease;
--transition-base: 0.2s ease;
--transition-slow: 0.3s ease;
```

### Common Animations

```css
/* Fade In */
@keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}

/* Slide Down */
@keyframes slideDown {
    from { transform: translateY(-10px); opacity: 0; }
    to { transform: translateY(0); opacity: 1; }
}

/* Pulse (for notifications) */
@keyframes pulse {
    0%, 100% { transform: scale(1); }
    50% { transform: scale(1.05); }
}
```

### Usage

```css
.fade-in { animation: fadeIn 0.3s ease; }
.slide-down { animation: slideDown 0.3s ease; }
```

---

## Loading States

### Spinner

```css
.spinner {
    border: 3px solid var(--gray-200);
    border-top-color: var(--primary-blue);
    border-radius: 50%;
    width: 40px;
    height: 40px;
    animation: spin 0.8s linear infinite;
}

@keyframes spin {
    to { transform: rotate(360deg); }
}
```

### Skeleton Loaders

```css
.skeleton {
    background: linear-gradient(90deg, var(--gray-200) 25%, var(--gray-100) 50%, var(--gray-200) 75%);
    background-size: 200% 100%;
    animation: loading 1.5s infinite;
    border-radius: 4px;
}

@keyframes loading {
    0% { background-position: 200% 0; }
    100% { background-position: -200% 0; }
}
```

---

## Usage Examples

### Example: Patient Dashboard Card

```html
<div class="card">
    <div class="card-header">
        <h3 class="card-title">Your Appointments</h3>
    </div>
    <div class="card-body">
        <div class="appointment-card upcoming">
            <h4>Dr. Smith - General Checkup</h4>
            <p class="text-muted">Tomorrow at 2:00 PM</p>
            <span class="badge badge-scheduled">Scheduled</span>
        </div>
    </div>
    <div class="card-footer">
        <button class="btn btn-primary">View All</button>
    </div>
</div>
```

### Example: Queue Status Display

```html
<div class="queue-card waiting">
    <div class="d-flex justify-content-between align-items-center">
        <div>
            <h5>John Doe</h5>
            <p class="text-small text-muted">Check-in: 10:30 AM</p>
        </div>
        <span class="badge badge-waiting">Waiting</span>
    </div>
</div>
```

---

## Implementation Checklist

When creating new components:

- [ ] Use colors from the defined palette
- [ ] Follow spacing system (multiples of 4px/8px)
- [ ] Apply appropriate typography styles
- [ ] Ensure WCAG 2.1 AA contrast ratios
- [ ] Add hover and focus states
- [ ] Include responsive breakpoints
- [ ] Use consistent border radius
- [ ] Apply proper shadows for elevation
- [ ] Add loading states where needed
- [ ] Test keyboard navigation
- [ ] Verify on mobile devices

---

## File Organization

### CSS Files Structure

```
wwwroot/
├── app.css                    # Global styles, CSS variables
├── Components/
│   └── Layout/
│       ├── MainLayout.razor.css
│       └── NavMenu.razor.css
└── Pages/
    └── [PageName].razor.css   # Page-specific styles
```

### Best Practices

1. **Global styles in app.css:** Variables, resets, utilities
2. **Component styles:** Scoped to component files
3. **Avoid inline styles:** Use classes
4. **Name classes semantically:** .appointment-card not .blue-border-card
5. **Use CSS custom properties:** For dynamic theming

---

## Support & Questions

If you need clarification on any design decisions or patterns:

1. Check this document first
2. Ask in the team Microsoft Teams channel
3. Tag the team lead if you need to propose changes to the design system

**Remember:** Consistency is more important than perfection. When in doubt, follow the patterns shown in this guide.

---

**Design System Owner:** Francois Matenda Tshibala (Team Lead)
**Contributors:** All team members
**Next Review:** End of Phase 2 (Feature Development)
