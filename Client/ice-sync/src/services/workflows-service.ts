import api from './api';

function getAll() {
  return fetch(api.workflows)
    .then(res => res.json())
    .catch(err => console.log('Handled error:' + err));
}

async function run(id: number) {
  return fetch(`${api.workflows}/${id}/run`, {
    method: 'POST',
  })
    .then(res => {
        if (res.ok) {
            return res.json();
        }

        throw new Error('Invalid input');
    });
}

export default { getAll, run }