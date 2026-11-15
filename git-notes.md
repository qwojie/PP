# Git notes

## Task 15 — Basic git

### Commands

git checkout -b feature/git-basics
git add README.md
git commit -m "feat: task 15 - add line in feature branch"
git checkout main
git merge feature/git-basics
git commit -m "docs: task 15 - update title in main"
git checkout feature/git-basics
git commit -m "docs: task 15 - update title in feature"
git checkout main
git merge feature/git-basics
git commit -m "fix: task 15 - resolve merge conflict"

### Output (git log --graph --oneline --all)

*   a9046ab (HEAD -> main) fix: task 15 - resolve merge conflict
|\
| * a66ac1f (feature/git-basics) docs: task 15 - update title in feature
* | 86c361b docs: task 15 - update title in main
|/
* 3eb8040 feat: task 15 - add line in feature branch
* 52c53b6 feat: sync project structure and level 1 tasks
* 582941e (origin/main) task 01 - implement the level menu and project structure
* 4f9e57e README.md
* faf736c feat: task 00 - implement main menu loop and safe input
* adab1df Initial commit

### What I learned

Научился создавать ветки, делать слияние (merge) и вручную разрешать конфликты при изменении одних и тех же строк файла.

## Task 35 — Branches & PR

### PR link

https://github.com

### Rebase result

```text
441ad41 (HEAD -> feature/rebase-demo) chore: rebase step 1
ba16815 (origin/main, main) Merge pull request #2 from qwojie/feature/task-35
cba8f6e (origin/feature/task-35, feature/task-35) feat: task 35 - register level 2 tasks in menu
5b64ad5 feat: add level 2 tasks 16-34
d0ea2ba chore: add gitignore and editorconfig
```

## Level 3 — Advanced Git (Tasks 46–50)

### Task 46 — git stash / stash pop

```text
git stash list (до pop):
stash@{0}: On main: wip: level 3 git notes
stash@{1}: WIP on feature/rebase-demo: 2e541b2 chore: rebase step 3

git stash list (после pop):
stash@{0}: WIP on feature/rebase-demo: 2e541b2 chore: rebase step 3
```

### Task 47 — git rebase -i (squash)

```text
git log --oneline (до сквоша):
5daf183 (HEAD -> feature/squash-demo) wip: squash step 4
becae72 wip: squash step 3
3baea93 wip: squash step 2
5e5b6f8 wip: squash step 1
88c9c14 (main) docs: start level 3 git notes

git log --oneline (после сквоша):
58f5e3f (HEAD -> feature/squash-demo) docs: squash demo notes
88c9c14 (main) docs: start level 3 git notes
```

### Task 48 — Теги (v1.0.0)

```text
git tag:
v1.0.0

git show v1.0.0:
tag v1.0.0
Tagger: qwojie
Date:   Sun Nov 15 16:27:03 2026 +0300

Release v1.0.0
```

### Task 49 — Восстановление через git reflog

```text
git reflog -n 5:
6d4c423 (HEAD -> main) HEAD@{0}: reset: moving to 6d4c423
58f5e3f (tag: v1.0.0, origin/main) HEAD@{1}: reset: moving to HEAD~1
6d4c423 (HEAD -> main) HEAD@{2}: commit: feat: important commit before reset

git log --oneline -n 2:
6d4c423 (HEAD -> main) feat: important commit before reset
58f5e3f (tag: v1.0.0, origin/main) docs: squash demo notes
```

### Task 50 — Поиск плохого коммита через git bisect

```text
8963afd22d4874d7e242ab0aab44af9647b8b217 is the first 'bad' commit
commit 8963afd22d4874d7e242ab0aab44af9647b8b217
Author: qwojie
Date:   Sun Nov 15 16:31:19 2026 +0300

    bisect step 3: update math
```
