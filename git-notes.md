\# Git notes

\## Task 15 — Basic git

\### Commands

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



\### Output (git log --graph --oneline --all)

\*   a9046ab (HEAD -> main) fix: task 15 - resolve merge conflict

|\\

| \* a66ac1f (feature/git-basics) docs: task 15 - update title in feature

\* | 86c361b docs: task 15 - update title in main

|/

\* 3eb8040 feat: task 15 - add line in feature branch

\* 52c53b6 feat: sync project structure and level 1 tasks

\* 582941e (origin/main) task 01 - implement the level menu and project structure

\* 4f9e57e README.md

\* faf736c feat: task 00 - implement main menu loop and safe input

\* adab1df Initial commit



\### What I learned

Научился создавать ветки, делать слияние (merge) и вручную разрешать конфликты при изменении одних и тех же строк файла.



