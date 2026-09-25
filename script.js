
/* ==========================================================
   NETWORK ENGINEER BACKGROUND ANIMATION
   Lightweight canvas animation; no external library required.
   ========================================================== */
(() => {
  const canvas = document.getElementById("networkCanvas");
  if (!canvas) return;

  const ctx = canvas.getContext("2d", { alpha: true });
  const reducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)");

  let width = 0;
  let height = 0;
  let dpr = 1;
  let nodes = [];
  let links = [];
  let packets = [];
  let animationId = null;
  let lastTime = 0;

  const COLORS = {
    line: "rgba(56, 189, 248, 0.16)",
    lineStrong: "rgba(56, 189, 248, 0.28)",
    node: "rgba(56, 189, 248, 0.82)",
    glow: "rgba(56, 189, 248, 0.20)",
    packet: "rgba(125, 211, 252, 0.95)"
  };

  function resize() {
    dpr = Math.min(window.devicePixelRatio || 1, 2);
    width = window.innerWidth;
    height = window.innerHeight;

    canvas.width = Math.floor(width * dpr);
    canvas.height = Math.floor(height * dpr);
    canvas.style.width = width + "px";
    canvas.style.height = height + "px";

    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    buildNetwork();
  }

  function makeNode(x, y, radius = 2.5) {
    return {
      x, y,
      baseX: x,
      baseY: y,
      radius,
      phase: Math.random() * Math.PI * 2,
      drift: 0.8 + Math.random() * 0.8
    };
  }

  function buildNetwork() {
    const mobile = width < 700;
    const count = mobile ? 20 : Math.min(34, Math.max(24, Math.round(width / 55)));

    nodes = [];

    // Seed nodes in a loose grid, then jitter them for a natural topology.
    const cols = mobile ? 4 : Math.max(5, Math.round(Math.sqrt(count * width / height)));
    const rows = Math.ceil(count / cols);
    const xStep = width / (cols + 1);
    const yStep = height / (rows + 1);

    let index = 0;
    for (let r = 1; r <= rows && index < count; r++) {
      for (let c = 1; c <= cols && index < count; c++) {
        const jitterX = (Math.random() - 0.5) * xStep * 0.55;
        const jitterY = (Math.random() - 0.5) * yStep * 0.55;
        nodes.push(
          makeNode(
            c * xStep + jitterX,
            r * yStep + jitterY,
            mobile ? 2.1 : 2.6
          )
        );
        index++;
      }
    }

    links = [];
    const maxDistance = mobile ? 230 : 270;

    for (let i = 0; i < nodes.length; i++) {
      const candidates = [];
      for (let j = 0; j < nodes.length; j++) {
        if (i === j) continue;
        const dx = nodes[i].x - nodes[j].x;
        const dy = nodes[i].y - nodes[j].y;
        const dist = Math.hypot(dx, dy);
        if (dist < maxDistance) candidates.push({ j, dist });
      }

      candidates.sort((a, b) => a.dist - b.dist);
      const nearest = candidates.slice(0, 2 + (Math.random() > 0.55 ? 1 : 0));

      for (const c of nearest) {
        const a = Math.min(i, c.j);
        const b = Math.max(i, c.j);
        const key = a + ":" + b;
        if (!links.some(link => link.key === key)) {
          links.push({ a, b, key });
        }
      }
    }

    packets = links
      .filter(() => Math.random() > 0.35)
      .slice(0, mobile ? 8 : 13)
      .map(link => ({
        link,
        progress: Math.random(),
        speed: 0.000035 + Math.random() * 0.00003,
        direction: Math.random() > 0.5 ? 1 : -1
      }));
  }

  function drawNode(node, time) {
    const pulse = 0.65 + Math.sin(time * 0.0018 * node.drift + node.phase) * 0.22;
    const radius = node.radius * (0.9 + pulse * 0.35);

    ctx.beginPath();
    ctx.fillStyle = COLORS.glow;
    ctx.arc(node.x, node.y, radius * 5.5, 0, Math.PI * 2);
    ctx.fill();

    ctx.beginPath();
    ctx.fillStyle = COLORS.node;
    ctx.arc(node.x, node.y, radius, 0, Math.PI * 2);
    ctx.fill();
  }

  function drawPacket(packet) {
    const a = nodes[packet.link.a];
    const b = nodes[packet.link.b];
    const p = packet.progress;

    const x = a.x + (b.x - a.x) * p;
    const y = a.y + (b.y - a.y) * p;

    ctx.beginPath();
    ctx.fillStyle = COLORS.packet;
    ctx.shadowBlur = 9;
    ctx.shadowColor = "rgba(56, 189, 248, 0.9)";
    ctx.arc(x, y, 2.2, 0, Math.PI * 2);
    ctx.fill();
    ctx.shadowBlur = 0;
  }

  function frame(time) {
    if (reducedMotion.matches) return;

    if (!lastTime) lastTime = time;
    const delta = Math.min(time - lastTime, 40);
    lastTime = time;

    ctx.clearRect(0, 0, width, height);

    // Very slow node drift.
    for (const node of nodes) {
      node.x = node.baseX + Math.sin(time * 0.00022 * node.drift + node.phase) * 5;
      node.y = node.baseY + Math.cos(time * 0.00018 * node.drift + node.phase) * 4;
    }

    for (const link of links) {
      const a = nodes[link.a];
      const b = nodes[link.b];

      ctx.beginPath();
      ctx.moveTo(a.x, a.y);
      ctx.lineTo(b.x, b.y);
      ctx.strokeStyle = Math.random() > 0.985 ? COLORS.lineStrong : COLORS.line;
      ctx.lineWidth = 1;
      ctx.stroke();
    }

    for (const node of nodes) drawNode(node, time);

    for (const packet of packets) {
      packet.progress += delta * packet.speed * packet.direction;

      if (packet.progress > 1) {
        packet.progress = 0;
        packet.direction = Math.random() > 0.5 ? 1 : -1;
      } else if (packet.progress < 0) {
        packet.progress = 1;
        packet.direction = Math.random() > 0.5 ? 1 : -1;
      }

      drawPacket(packet);
    }

    animationId = requestAnimationFrame(frame);
  }

  function start() {
    if (animationId) cancelAnimationFrame(animationId);
    lastTime = 0;
    if (!reducedMotion.matches) {
      animationId = requestAnimationFrame(frame);
    }
  }

  window.addEventListener("resize", resize, { passive: true });
  reducedMotion.addEventListener?.("change", start);

  resize();
  start();
})();

/* ==========================================================
   Existing resume interactions
   ========================================================== */

function openLightbox() {
  const src = document.getElementById("avatarImg").src;
  document.getElementById("lightboxImg").src = src;
  document.getElementById("lightbox").classList.add("open");
}

function closeLightbox(e) {
  if (e) e.stopPropagation();
  document.getElementById("lightbox").classList.remove("open");
}

document.addEventListener("keydown", (e) => {
  if (e.key === "Escape") closeLightbox();
});

/* Typewriter effect for role title */
const roleTitles = ["NOC Engineer", "Network Engineer"];
const roleEl = document.getElementById("roleTyped");
const prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

if (roleEl) {
  if (prefersReducedMotion) {
    roleEl.textContent = roleTitles[0] + " / " + roleTitles[1];
  } else {
    let titleIndex = 0;
    let charIndex = 0;
    let deleting = false;

    function tick() {
      const current = roleTitles[titleIndex];

      if (!deleting) {
        charIndex++;
        roleEl.textContent = current.slice(0, charIndex);

        if (charIndex === current.length) {
          deleting = true;
          setTimeout(tick, 1400);
          return;
        }
      } else {
        charIndex--;
        roleEl.textContent = current.slice(0, charIndex);

        if (charIndex === 0) {
          deleting = false;
          titleIndex = (titleIndex + 1) % roleTitles.length;
        }
      }

      setTimeout(tick, deleting ? 35 : 65);
    }

    tick();
  }
}

/* Tabs */
const tabs = document.querySelectorAll("nav.tabs a");
const panels = document.querySelectorAll(".panel");

tabs.forEach(tab => {
  tab.addEventListener("click", (e) => {
    e.preventDefault();
    const target = tab.getAttribute("data-target");

    tabs.forEach(t => t.classList.remove("active"));
    tab.classList.add("active");

    panels.forEach(p => p.classList.toggle("active", p.id === target));

    history.replaceState(null, "", "#" + target);
    window.scrollTo({ top: 0, behavior: "smooth" });
  });
});

/* Support direct links like page.html#skills */
const initial = window.location.hash.replace("#", "");
if (initial) {
  const match = document.querySelector('nav.tabs a[data-target="' + initial + '"]');
  if (match) match.click();
}
