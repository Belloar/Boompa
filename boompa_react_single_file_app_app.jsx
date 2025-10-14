// App.jsx
// Single-file React app for "Boompa" — a kid-friendly learning + games web app.
// Uses Tailwind classes for styling. Default export is App.
// Requirements: react, react-dom, react-router-dom, Tailwind CSS configured in your project.

import React, { useEffect, useState } from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  useNavigate,
  useParams,
} from "react-router-dom";

// -----------------------------
// Mock & API helpers
// -----------------------------

// Replace `API_BASE` with your real endpoint, e.g. https://api.example.com
const API_BASE = process.env.REACT_APP_API_BASE || "/api";

async function apiFetch(path, opts = {}) {
  // Basic wrapper: fetch and handle JSON, with simple error handling.
  try {
    const res = await fetch(`${API_BASE}${path}`, {
      headers: { "Content-Type": "application/json" },
      ...opts,
    });
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    return await res.json();
  } catch (err) {
    console.warn("API fetch failed:", err);
    // fallback: return null to allow UI to show placeholder
    return null;
  }
}

// Local mock for demo if API isn't present
const MOCK_CATEGORIES = [
  { id: "history", name: "History" },
  { id: "biology", name: "Biology" },
  { id: "mental-math", name: "Mental Math" },
  { id: "abstract-thinking", name: "Abstract Thinking" },
  { id: "verbal-reasoning", name: "Verbal Reasoning" },
  { id: "space", name: "Space & Astronomy" },
];

const MOCK_ARTICLES = {
  history: [
    { id: 1, title: "Kings & Queens: A Short Story", excerpt: "A gentle intro to ancient rulers..." },
  ],
  biology: [
    { id: 2, title: "Plants 101", excerpt: "Where do plants get food? Let's explore..." },
  ],
  "mental-math": [
    { id: 3, title: "Quick Addition Tricks", excerpt: "Mental math tricks for ages 7+" },
  ],
};

// -----------------------------
// Tiny Auth + Profile (mocked)
// -----------------------------

function useAuth() {
  const [user, setUser] = useState(() => {
    const raw = localStorage.getItem("boompa_user");
    return raw ? JSON.parse(raw) : null;
  });

  const signup = (profile) => {
    // profile: { name, age }
    const newUser = {
      id: Date.now(),
      name: profile.name,
      age: profile.age,
      rank: "Rookie",
      tickets: 10,
      diamonds: 0,
      coins: 50,
      isAdmin: profile.isAdmin || false,
    };
    localStorage.setItem("boompa_user", JSON.stringify(newUser));
    setUser(newUser);
  };

  const signout = () => {
    localStorage.removeItem("boompa_user");
    setUser(null);
  };

  return { user, signup, signout, setUser };
}

// -----------------------------
// Small UI primitives
// -----------------------------

function IconTicket() {
  return (
    <svg className="w-5 h-5 inline-block" viewBox="0 0 24 24" fill="none">
      <path d="M3 8v8a1 1 0 001 1h16a1 1 0 001-1V8" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
      <circle cx="9" cy="12" r="1" fill="currentColor" />
      <circle cx="15" cy="12" r="1" fill="currentColor" />
    </svg>
  );
}

function TopBar({ user, onSignOut }) {
  return (
    <div className="bg-gradient-to-r from-indigo-500 to-purple-500 text-white p-3 flex items-center justify-between">
      <div className="flex items-center gap-3">
        <Link to="/" className="font-extrabold text-xl">Boompa</Link>
        <span className="hidden sm:inline opacity-90">— Learn, Play, Grow</span>
      </div>

      <div className="flex items-center gap-4">
        {user ? (
          <>
            <div className="text-sm text-right">
              <div className="font-medium">{user.name}</div>
              <div className="text-xs opacity-90">Rank: {user.rank}</div>
            </div>

            <div className="flex items-center gap-3 bg-white/10 rounded-lg px-3 py-1 text-sm">
              <div className="flex items-center gap-1"> <IconTicket /> {user.tickets}</div>
              <div className="flex items-center gap-1">💎 {user.diamonds}</div>
              <div className="flex items-center gap-1">🪙 {user.coins}</div>
            </div>

            <button
              onClick={onSignOut}
              className="bg-white text-indigo-600 px-3 py-1 rounded-full text-sm font-semibold hover:opacity-90"
            >
              Sign out
            </button>
          </>
        ) : (
          <div className="flex items-center gap-2">
            <Link to="/signup" className="bg-white text-indigo-600 px-3 py-1 rounded-full text-sm font-semibold">Create account</Link>
          </div>
        )}
      </div>
    </div>
  );
}

// -----------------------------
// Pages
// -----------------------------

function Home() {
  return (
    <div className="max-w-5xl mx-auto p-6">
      <header className="flex flex-col sm:flex-row items-center justify-between gap-6">
        <div>
          <h1 className="text-4xl font-extrabold">Welcome to <span className="text-indigo-600">Boompa</span></h1>
          <p className="mt-2 text-gray-700">A safe, colourful learning and gaming playground for kids aged 5–15. Explore fun lessons, play educational games, and earn rewards!</p>

          <div className="mt-4 flex gap-3">
            <Link to="/signup" className="bg-indigo-600 text-white px-4 py-2 rounded-lg font-semibold">Create account — it's free</Link>
            <Link to="/learning" className="border border-indigo-600 text-indigo-600 px-4 py-2 rounded-lg">Explore learning</Link>
          </div>
        </div>

        <div className="w-full sm:w-1/2">
          <div className="bg-white rounded-2xl p-4 shadow-lg">
            <h3 className="font-bold">How Boompa works</h3>
            <ul className="mt-2 space-y-2 text-sm text-gray-700">
              <li>• Children choose subjects and read short, illustrated articles.</li>
              <li>• Complete quizzes and mini-games to earn coins, diamonds and tickets.</li>
              <li>• Parents or admins can create content in the Admin Creator.</li>
            </ul>
          </div>
        </div>
      </header>

      <section className="mt-8 grid sm:grid-cols-3 gap-4">
        <FeatureCard title="Playful learning" desc="Lessons designed with movement, stories and pictures to keep kids engaged." emoji="📚" />
        <FeatureCard title="Rewards" desc="Earn coins, diamonds and tickets for completing activities." emoji="🎟️" />
        <FeatureCard title="Safe for kids" desc="No ads, parental controls and simple language." emoji="🔒" />
      </section>

      <section className="mt-8">
        <h2 className="text-2xl font-bold">Popular categories</h2>
        <div className="mt-3 flex flex-wrap gap-3">
          {MOCK_CATEGORIES.map(c => (
            <Link key={c.id} to={`/learning/${c.id}`} className="px-3 py-2 rounded-xl bg-indigo-50 text-indigo-700 font-medium">{c.name}</Link>
          ))}
        </div>
      </section>
    </div>
  );
}

function FeatureCard({ title, desc, emoji }) {
  return (
    <div className="bg-white rounded-2xl p-4 shadow hover:shadow-md transition">
      <div className="text-3xl">{emoji}</div>
      <h3 className="font-semibold mt-2">{title}</h3>
      <p className="mt-1 text-sm text-gray-600">{desc}</p>
    </div>
  );
}

function Signup() {
  const navigate = useNavigate();
  const { signup } = React.useContext(AuthContext);
  const [name, setName] = useState("");
  const [age, setAge] = useState(8);
  const [isAdmin, setIsAdmin] = useState(false);

  const handle = (e) => {
    e.preventDefault();
    // Minimal validation
    if (!name || !age) return alert("Please fill name and age");
    signup({ name, age: Number(age), isAdmin });
    navigate("/dashboard");
  };

  return (
    <div className="max-w-2xl mx-auto p-6">
      <h2 className="text-2xl font-bold mb-4">Create an account</h2>
      <form onSubmit={handle} className="space-y-3 bg-white p-4 rounded-lg shadow">
        <label className="block">
          <div className="text-sm font-medium">Child's name</div>
          <input value={name} onChange={(e)=>setName(e.target.value)} className="mt-1 w-full border rounded px-3 py-2" placeholder="e.g. Aisha" />
        </label>

        <label className="block">
          <div className="text-sm font-medium">Age</div>
          <input type="number" min={5} max={15} value={age} onChange={(e)=>setAge(e.target.value)} className="mt-1 w-32 border rounded px-3 py-2" />
        </label>

        <label className="flex items-center gap-2">
          <input type="checkbox" checked={isAdmin} onChange={(e)=>setIsAdmin(e.target.checked)} />
          <div className="text-xs text-gray-600">Sign up as admin (for demo only)</div>
        </label>

        <div className="flex gap-2">
          <button className="bg-indigo-600 text-white px-4 py-2 rounded">Create account</button>
          <Link to="/" className="px-4 py-2 border rounded">Back</Link>
        </div>
      </form>
    </div>
  );
}

function Dashboard() {
  const { user, signout } = React.useContext(AuthContext);
  const navigate = useNavigate();

  if (!user) return (
    <div className="p-6">You must <Link to="/signup" className="text-indigo-600">create an account</Link> first.</div>
  );

  return (
    <div className="max-w-4xl mx-auto p-6">
      <div className="bg-white rounded-xl p-6 shadow">
        <div className="flex items-center justify-between">
          <div>
            <h2 className="text-2xl font-bold">Hello, {user.name} 👋</h2>
            <p className="text-sm text-gray-600">Choose an activity below</p>
          </div>

          <div className="text-sm text-right">
            <div className="font-semibold">Rank: {user.rank}</div>
            <div className="text-xs text-gray-600">Tickets: {user.tickets} · Diamonds: {user.diamonds} · Coins: {user.coins}</div>
          </div>
        </div>

        <div className="mt-6 grid sm:grid-cols-2 gap-4">
          <CardAction title="Learn" desc="Explore lessons and articles" cta="Start learning" onClick={() => navigate('/learning')} />
          <CardAction title="Games" desc="Play fun educational games" cta="Play games" onClick={() => navigate('/games')} />
        </div>

        {user.isAdmin && (
          <div className="mt-6">
            <Link to="/create-content" className="px-3 py-2 bg-amber-400 rounded-lg font-semibold">Admin: Create learning content</Link>
          </div>
        )}
      </div>
    </div>
  );
}

function CardAction({ title, desc, cta, onClick }) {
  return (
    <div className="bg-gradient-to-tr from-indigo-50 to-white rounded-2xl p-4 shadow hover:shadow-md">
      <h3 className="font-bold text-lg">{title}</h3>
      <p className="mt-1 text-sm text-gray-600">{desc}</p>
      <div className="mt-3">
        <button onClick={onClick} className="bg-indigo-600 text-white px-3 py-2 rounded-lg font-semibold">{cta}</button>
      </div>
    </div>
  );
}

function Learning() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    (async () => {
      const data = await apiFetch('/categories');
      if (data && Array.isArray(data)) setCategories(data);
      else setCategories(MOCK_CATEGORIES);
    })();
  }, []);

  return (
    <div className="max-w-5xl mx-auto p-6">
      <h2 className="text-2xl font-bold">Pick a category</h2>
      <p className="text-sm text-gray-600 mt-1">Tap a topic to see short articles and activities.</p>

      <div className="mt-4 grid sm:grid-cols-3 gap-4">
        {categories.map(cat => (
          <Link key={cat.id} to={`/learning/${cat.id}`} className="bg-white rounded-xl p-4 shadow hover:shadow-md">
            <div className="text-2xl">📘</div>
            <div className="font-semibold mt-2">{cat.name}</div>
            <div className="text-sm text-gray-500 mt-1">Click to explore</div>
          </Link>
        ))}
      </div>
    </div>
  );
}

function CategoryPage() {
  const { id } = useParams();
  const [articles, setArticles] = useState([]);
  const [category, setCategory] = useState(null);

  useEffect(() => {
    (async () => {
      const catData = await apiFetch(`/categories/${id}`);
      const articleData = await apiFetch(`/categories/${id}/articles`);

      if (catData) setCategory(catData);
      else setCategory(MOCK_CATEGORIES.find(c=>c.id===id) || { id, name: id });

      if (articleData && Array.isArray(articleData)) setArticles(articleData);
      else setArticles(MOCK_ARTICLES[id] || []);
    })();
  }, [id]);

  return (
    <div className="max-w-4xl mx-auto p-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold">{category ? category.name : id}</h2>
      </div>

      <div className="mt-4 space-y-3">
        {articles.length === 0 && (
          <div className="p-4 bg-yellow-50 rounded">No articles yet — ask an adult to create content!</div>
        )}

        {articles.map(a => (
          <ArticleCard key={a.id} article={a} categoryId={id} />
        ))}
      </div>
    </div>
  );
}

function ArticleCard({ article, categoryId }) {
  return (
    <div className="bg-white rounded-lg p-4 shadow flex items-start gap-4">
      <div className="w-16 h-16 bg-indigo-50 rounded-md flex items-center justify-center text-2xl">📄</div>
      <div>
        <div className="font-semibold">{article.title}</div>
        <div className="text-sm text-gray-600">{article.excerpt}</div>
        <div className="mt-2">
          <Link to={`/learning/${categoryId}/article/${article.id}`} className="text-indigo-600 font-medium text-sm">Read article</Link>
        </div>
      </div>
    </div>
  );
}

function ArticlePage() {
  const { id, articleId } = useParams();
  const [article, setArticle] = useState(null);

  useEffect(() => {
    (async () => {
      const data = await apiFetch(`/categories/${id}/articles/${articleId}`);
      if (data) setArticle(data);
      else {
        // search in mock
        const arr = MOCK_ARTICLES[id] || [];
        setArticle(arr.find(a => String(a.id) === String(articleId)) || { title: "Missing article", body: "No content found." });
      }
    })();
  }, [id, articleId]);

  if (!article) return <div className="p-6">Loading...</div>;

  return (
    <div className="max-w-3xl mx-auto p-6">
      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-2xl font-bold">{article.title}</h2>
        <div className="mt-4 text-gray-700" dangerouslySetInnerHTML={{ __html: article.body || article.excerpt || "" }} />
        {/* In production avoid dangerouslySetInnerHTML or sanitize content from API */}
      </div>
    </div>
  );
}

function Games() {
  const navigate = useNavigate();
  // Simple games listing
  const games = [
    { id: 'math-quiz', name: 'Math Quickfire', desc: 'Solve quick math for coins' },
    { id: 'memory', name: 'Memory Match', desc: 'Match pairs to earn tickets' },
  ];

  return (
    <div className="max-w-4xl mx-auto p-6">
      <h2 className="text-2xl font-bold">Games</h2>
      <div className="mt-4 grid sm:grid-cols-2 gap-4">
        {games.map(g => (
          <div key={g.id} className="bg-white p-4 rounded-lg shadow hover:shadow-md">
            <div className="font-semibold">{g.name}</div>
            <div className="text-sm text-gray-600">{g.desc}</div>
            <div className="mt-3">
              <button onClick={() => navigate(`/games/${g.id}`)} className="bg-indigo-600 text-white px-3 py-2 rounded">Play</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

function MiniGame() {
  const { gameId } = useParams();
  return (
    <div className="p-6">
      <h2 className="text-2xl font-bold">{gameId}</h2>
      <p className="text-gray-600 mt-2">This is a placeholder for the mini-game. Integrate your game engines or build React game components here (Canvas, Phaser, or simple DOM games).</p>
    </div>
  );
}

function CreateContent() {
  const { user } = React.useContext(AuthContext);
  const navigate = useNavigate();
  const [category, setCategory] = useState("");
  const [title, setTitle] = useState("");
  const [excerpt, setExcerpt] = useState("");
  const [body, setBody] = useState("");
  const [saving, setSaving] = useState(false);

  useEffect(()=>{
    if (!user || !user.isAdmin) {
      // not authorized; redirect to dashboard after a tiny delay
      const t = setTimeout(()=>navigate('/dashboard'), 800);
      return ()=>clearTimeout(t);
    }
  }, [user, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!category || !title) return alert('Category and title are required');

    setSaving(true);
    const payload = { category, title, excerpt, body };
    const res = await apiFetch(`/categories/${category}/articles`, { method: 'POST', body: JSON.stringify(payload) });
    setSaving(false);

    if (res === null) {
      alert('Failed to save to API — for demo content saved locally');
      // In demo mode, persist to localStorage
      const stored = JSON.parse(localStorage.getItem('mock_articles') || '{}');
      stored[category] = stored[category] || [];
      stored[category].push({ id: Date.now(), title, excerpt, body });
      localStorage.setItem('mock_articles', JSON.stringify(stored));
      navigate(`/learning/${category}`);
    } else {
      alert('Saved!');
      navigate(`/learning/${category}`);
    }
  };

  return (
    <div className="max-w-3xl mx-auto p-6">
      <h2 className="text-2xl font-bold">Create learning content</h2>
      <form onSubmit={handleSubmit} className="mt-4 bg-white p-4 rounded-lg shadow space-y-3">
        <label className="block">
          <div className="text-sm font-medium">Category (id)</div>
          <input value={category} onChange={(e)=>setCategory(e.target.value)} placeholder="e.g. history" className="mt-1 w-full border rounded px-3 py-2" />
          <div className="text-xs text-gray-500 mt-1">Use an existing category id or create a new one. Examples: history, biology, mental-math</div>
        </label>

        <label className="block">
          <div className="text-sm font-medium">Title</div>
          <input value={title} onChange={(e)=>setTitle(e.target.value)} className="mt-1 w-full border rounded px-3 py-2" />
        </label>

        <label className="block">
          <div className="text-sm font-medium">Short excerpt</div>
          <input value={excerpt} onChange={(e)=>setExcerpt(e.target.value)} className="mt-1 w-full border rounded px-3 py-2" />
        </label>

        <label className="block">
          <div className="text-sm font-medium">Body (HTML allowed)</div>
          <textarea value={body} onChange={(e)=>setBody(e.target.value)} rows={8} className="mt-1 w-full border rounded px-3 py-2" />
        </label>

        <div className="flex gap-2">
          <button className="bg-emerald-500 text-white px-4 py-2 rounded" disabled={saving}>{saving ? 'Saving...' : 'Save article'}</button>
          <Link to="/dashboard" className="px-4 py-2 border rounded">Cancel</Link>
        </div>
      </form>
    </div>
  );
}

// -----------------------------
// App root & routing
// -----------------------------

const AuthContext = React.createContext();

export default function App() {
  const auth = useAuth();

  return (
    <AuthContext.Provider value={{ ...auth, user: auth.user }}>
      <Router>
        <div className="min-h-screen bg-gray-50 text-gray-800">
          <TopBar user={auth.user} onSignOut={auth.signout} />

          <main className="py-6">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/signup" element={<Signup />} />
              <Route path="/dashboard" element={<Dashboard />} />
              <Route path="/learning" element={<Learning />} />
              <Route path="/learning/:id" element={<CategoryPage />} />
              <Route path="/learning/:id/article/:articleId" element={<ArticlePage />} />
              <Route path="/create-content" element={<CreateContent />} />
              <Route path="/games" element={<Games />} />
              <Route path="/games/:gameId" element={<MiniGame />} />

              <Route path="*" element={<NotFound />} />
            </Routes>
          </main>

          <footer className="text-center text-sm text-gray-500 py-6">© {new Date().getFullYear()} Boompa — made with ❤️ for curious kids</footer>
        </div>
      </Router>
    </AuthContext.Provider>
  );
}

function NotFound() {
  return (
    <div className="p-6 text-center">
      <h2 className="font-bold text-2xl">Page not found</h2>
      <p className="mt-2">Try the <Link to="/" className="text-indigo-600">homepage</Link>.</p>
    </div>
  );
}

// -----------------------------
// Notes for integrators (keep in README)
// -----------------------------
/*
Installation notes:
1. Create a React app (Vite or CRA). Copy this file as src/App.jsx and ensure React Router is installed:
   npm install react-router-dom

2. Tailwind: configure Tailwind in your project. This file uses Tailwind utility classes.

3. API Endpoints expected (examples):
   GET  /api/categories                -> [{ id, name }, ...]
   GET  /api/categories/:id            -> { id, name }
   GET  /api/categories/:id/articles   -> [{ id, title, excerpt }, ...]
   GET  /api/categories/:id/articles/:articleId -> { id, title, body, excerpt }
   POST /api/categories/:id/articles   -> create article

4. For production, replace localStorage-based mock and remove dangerouslySetInnerHTML or sanitize content from API.

5. Games: integrate actual game code inside components (Canvas, Phaser, or plain React mini games).
*/
