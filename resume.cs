:root {
    --bg: #0B1220;
    --panel: #121B2E;
    --border: #1C2942;
    --text: #E7ECF5;
    --muted: #8A96AC;
    --accent-green: #4ADE80;
    --accent-blue: #38BDF8;
    --font-display: 'Space Grotesk', sans-serif;
    --font-body: 'Inter', -apple-system, sans-serif;
    --font-mono: 'JetBrains Mono', ui-monospace, monospace;
  }

  :root[data-theme="light"] {
    --bg: #F7F8FB;
    --panel: #FFFFFF;
    --border: #E1E5EE;
    --text: #16202F;
    --muted: #5B6577;
    --accent-green: #1B9C5A;
    --accent-blue: #0B77C2;
  }

  * { box-sizing: border-box; }

  html {
    scroll-padding-top: env(safe-area-inset-top, 0px);
  }

  body {
    margin: 0;
    background: var(--bg);
    color: var(--text);
    font-family: var(--font-body);
    line-height: 1.6;
    padding-top: env(safe-area-inset-top, 0px);
    padding-bottom: env(safe-area-inset-bottom, 0px);
    -webkit-font-smoothing: antialiased;
  }

  a { color: var(--accent-blue); text-decoration: none; }
  a:hover { text-decoration: underline; }
  a:focus-visible, button:focus-visible { outline: 2px solid var(--accent-blue); outline-offset: 3px; }

  .wrap {
    max-width: 760px;
    margin: 0 auto;
    padding: 0 28px;
  }

  nav.tabs {
    position: sticky;
    top: env(safe-area-inset-top, 0px);
    z-index: 10;
    background: var(--bg);
    border-bottom: 1px solid var(--border);
  }

  nav.tabs .wrap {
    display: flex;
    gap: 4px;
    padding-left: 28px;
    padding-right: 28px;
    overflow-x: auto;
    scrollbar-width: none;
  }

  nav.tabs .wrap::-webkit-scrollbar { display: none; }

  nav.tabs a {
    font-family: var(--font-mono);
    font-size: 12.5px;
    color: var(--muted);
    padding: 16px 14px;
    white-space: nowrap;
    border-bottom: 2px solid transparent;
    cursor: pointer;
  }

  nav.tabs a:hover {
    color: var(--text);
    text-decoration: none;
  }

  nav.tabs a.active {
    color: var(--text);
    border-bottom-color: var(--accent-blue);
  }

  .panel { display: none; }
  .panel.active { display: block; }

  /* ---------- Hero ---------- */
  .avatar {
    width: 220px;
    height: 220px;
    border-radius: 50%;
    object-fit: cover;
    border: 1px solid var(--border);
    margin: 0 auto 28px;
    display: block;
    cursor: zoom-in;
    transition: opacity 0.15s ease;
  }

  .avatar:hover {
    opacity: 0.9;
  }

  .lightbox {
    display: none;
    position: fixed;
    inset: 0;
    background: rgba(6, 10, 18, 0.88);
    z-index: 100;
    align-items: center;
    justify-content: center;
    padding: 32px;
    cursor: zoom-out;
  }

  .lightbox.open {
    display: flex;
  }

  .lightbox img {
    max-width: min(80vw, 480px);
    max-height: 80vh;
    border-radius: 12px;
    border: 1px solid var(--border);
    box-shadow: 0 20px 60px rgba(0, 0, 0, 0.5);
  }

  .lightbox-close {
    position: absolute;
    top: env(safe-area-inset-top, 20px);
    right: 24px;
    font-family: var(--font-mono);
    font-size: 13px;
    color: var(--muted);
    background: none;
    border: 1px solid var(--border);
    border-radius: 4px;
    padding: 6px 12px;
    cursor: pointer;
  }

  .lightbox-close:hover {
    color: var(--text);
  }

  .hero {
    padding: 56px 0 8px;
    text-align: center;
  }

  .status {
    display: inline-flex;
    align-items: center;
    gap: 8px;
    font-family: var(--font-mono);
    font-size: 12.5px;
    color: var(--accent-green);
    letter-spacing: 0.02em;
    margin-bottom: 28px;
    justify-content: center;
  }

  .status .dot {
    width: 7px;
    height: 7px;
    border-radius: 50%;
    background: var(--accent-green);
    box-shadow: 0 0 0 0 rgba(74, 222, 128, 0.55);
    animation: pulse 2.4s ease-out infinite;
  }

  @media (prefers-reduced-motion: reduce) {
    .status .dot { animation: none; }
  }

  @keyframes pulse {
    0%   { box-shadow: 0 0 0 0 rgba(74, 222, 128, 0.45); }
    70%  { box-shadow: 0 0 0 8px rgba(74, 222, 128, 0); }
    100% { box-shadow: 0 0 0 0 rgba(74, 222, 128, 0); }
  }

  h1 {
    font-family: var(--font-display);
    font-weight: 700;
    font-size: clamp(32px, 6vw, 46px);
    line-height: 1.12;
    margin: 0 0 10px;
    letter-spacing: -0.01em;
  }

  .role {
    font-family: var(--font-display);
    font-weight: 500;
    font-size: 18px;
    color: var(--muted);
    margin: 0 0 10px;
    min-height: 26px;
  }

  .cursor {
    color: var(--accent-green);
    font-weight: 400;
    animation: blink 1s step-end infinite;
  }

  @keyframes blink {
    50% { opacity: 0; }
  }

  @media (prefers-reduced-motion: reduce) {
    .cursor { animation: none; }
  }

  .motto {
    font-family: var(--font-mono);
    font-size: 13.5px;
    font-style: italic;
    color: var(--accent-green);
    margin: 0 0 22px;
  }

  .summary {
    max-width: 58ch;
    font-size: 16px;
    color: var(--text);
    margin: 0 auto 28px;
  }

  .contact-row {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    align-items: center;
    gap: 10px;
    font-family: var(--font-mono);
    font-size: 13px;
    color: var(--muted);
  }

  .contact-row > *:not(:last-child)::after {
    content: "|";
    color: var(--border);
    margin-left: 10px;
  }

  .icon-link {
    display: inline-flex !important;
    align-items: center;
    gap: 6px;
  }

  .icon {
    width: 14px;
    height: 14px;
    flex-shrink: 0;
  }

  .languages {
    font-family: var(--font-mono);
    font-size: 13px;
    color: var(--muted);
    margin: 14px 0 0;
  }

  .contact-row a { color: var(--muted); }
  .contact-row a:hover { color: var(--accent-blue); }

  /* ---------- Sections ---------- */
  section { padding: 48px 0; border-bottom: 1px solid var(--border); }
  section:last-of-type { border-bottom: none; }

  h2 {
    font-family: var(--font-display);
    font-weight: 600;
    font-size: 13px;
    text-transform: none;
    letter-spacing: 0.02em;
    color: var(--muted);
    margin: 0 0 28px;
    display: flex;
    align-items: baseline;
    gap: 10px;
  }

  h2::after {
    content: "";
    flex: 1;
    height: 1px;
    background: var(--border);
  }

  /* ---------- Timeline ---------- */
  .timeline { position: relative; }

  .entry {
    position: relative;
    padding-left: 26px;
    padding-bottom: 40px;
  }

  .entry:last-child { padding-bottom: 0; }

  .entry::before {
    content: "";
    position: absolute;
    left: 4px;
    top: 6px;
    width: 8px;
    height: 8px;
    border-radius: 50%;
    background: var(--accent-blue);
  }

  .entry::after {
    content: "";
    position: absolute;
    left: 7.5px;
    top: 18px;
    bottom: -8px;
    width: 1px;
    background: var(--border);
  }

  .entry:last-child::after { display: none; }

  .entry-head {
    display: flex;
    justify-content: space-between;
    align-items: baseline;
    flex-wrap: wrap;
    gap: 6px 16px;
    margin-bottom: 4px;
  }

  .org {
    font-family: var(--font-display);
    font-weight: 600;
    font-size: 17px;
  }

  .dates {
    font-family: var(--font-mono);
    font-size: 12.5px;
    color: var(--muted);
    white-space: nowrap;
  }

  .title-line {
    font-size: 14.5px;
    color: var(--accent-blue);
    margin: 0 0 14px;
  }

  ul.results {
    margin: 0;
    padding: 0;
    list-style: none;
  }

  ul.results li {
    position: relative;
    padding-left: 18px;
    margin-bottom: 10px;
    font-size: 15px;
    color: var(--text);
  }

  ul.results li::before {
    content: "→";
    position: absolute;
    left: 0;
    color: var(--muted);
    font-size: 13px;
    top: 2px;
  }

  ul.results li:last-child { margin-bottom: 0; }

  /* ---------- Skills ---------- */
  .skill-row {
    display: grid;
    grid-template-columns: 168px 1fr;
    gap: 6px 20px;
    padding: 12px 0;
    border-top: 1px solid var(--border);
  }

  .skill-row:first-of-type { border-top: none; padding-top: 0; }

  .skill-label {
    font-family: var(--font-mono);
    font-size: 12.5px;
    color: var(--muted);
    padding-top: 2px;
  }

  .skill-values {
    display: flex;
    flex-wrap: wrap;
    gap: 8px 10px;
    font-size: 14.5px;
  }

  .skill-values span {
    color: var(--text);
  }

  .skill-values span:not(:last-child)::after {
    content: "·";
    margin-left: 10px;
    color: var(--border);
  }

  @media (max-width: 520px) {
    .skill-row { grid-template-columns: 1fr; gap: 4px; }
  }

  /* ---------- Education / misc grid ---------- */
  .fact-grid {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 28px;
  }

  @media (max-width: 560px) {
    .fact-grid { grid-template-columns: 1fr; }
  }

  .fact h3 {
    font-family: var(--font-display);
    font-size: 16px;
    font-weight: 600;
    margin: 0 0 4px;
  }

  .fact p {
    margin: 0;
    font-size: 14.5px;
    color: var(--muted);
  }

  .fact .dates {
    display: block;
    margin-top: 6px;
  }

  .empty-state {
    padding: 20px 22px;
    border: 1px dashed var(--border);
    border-radius: 3px;
    font-size: 14px;
    color: var(--muted);
  }

  .empty-state strong {
    display: block;
    font-family: var(--font-display);
    font-weight: 600;
    font-size: 14px;
    color: var(--text);
    margin-bottom: 4px;
  }

  /* ---------- Footer ---------- */
  footer {
    padding: 44px 0 80px;
    font-size: 13px;
    color: var(--muted);
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
    gap: 10px;
  }

  footer a { color: var(--muted); }
  footer a:hover { color: var(--accent-blue); }


/* ---------- Animated network background ---------- */
body {
  position: relative;
  overflow-x: hidden;
}

.network-canvas {
  position: fixed;
  inset: 0;
  width: 100%;
  height: 100%;
  z-index: 0;
  pointer-events: none;
  opacity: 0.62;
}

.network-vignette {
  position: fixed;
  inset: 0;
  z-index: 0;
  pointer-events: none;
  background:
    radial-gradient(circle at 50% 42%, rgba(11, 18, 32, 0.00) 0%, rgba(11, 18, 32, 0.14) 46%, rgba(11, 18, 32, 0.54) 100%);
}

nav.tabs,
.wrap,
.lightbox {
  position: relative;
}

nav.tabs,
.wrap {
  z-index: 1;
}

.lightbox {
  z-index: 100;
}

/* Keep the network subtle behind the resume content. */
.hero,
.panel,
footer {
  position: relative;
}

@media (max-width: 700px) {
  .network-canvas {
    opacity: 0.36;
  }

  .network-vignette {
    background:
      radial-gradient(circle at 50% 35%, rgba(11, 18, 32, 0.05) 0%, rgba(11, 18, 32, 0.28) 52%, rgba(11, 18, 32, 0.70) 100%);
  }
}

@media (prefers-reduced-motion: reduce) {
  .network-canvas {
    display: none;
  }

  .network-vignette {
    display: none;
  }
}
