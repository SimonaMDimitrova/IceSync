import Workflows from './components/Workflows';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <>
      <header>
        <nav className="navbar navbar-dark bg-dark">
          <a className="navbar-brand" href="#">The Ice Cream Company</a>
        </nav>
      </header>
      <main>
        <Workflows/>
      </main>
      <footer className="bg-light text-center py-3 fixed-bottom">
        <span>&copy; 2025 The Ice Cream Company</span>
      </footer>
    </>
  );
}

export default App;
