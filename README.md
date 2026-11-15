# Chasing Shadows: Unity license test

A near-empty Unity project whose only job is to answer one question: can GitHub build a Unity APK for this account without a second computer?

## Steps

1. Sign in at https://id.unity.com (create a free Unity ID if needed). Use a password you do not use anywhere else.
2. Create a new GitHub repository, for example `chasing-shadows-unity`, and upload everything in this folder, including `.github`.
   If `.github` will not upload by drag and drop, use **Add file > Create new file**, name it `.github/workflows/unity-license-test.yml`, and paste that file's contents.
3. In the repo go to **Settings > Secrets and variables > Actions > New repository secret** and add:
   - `UNITY_EMAIL`: the Unity ID email
   - `UNITY_PASSWORD`: the Unity ID password
4. Open the **Actions** tab, pick **Unity license test**, and press **Run workflow**.

## Reading the result

| Job 1 (activate) | Job 2 (build) | Meaning |
|---|---|---|
| Green | Green | Unity builds work. Download `unity-test-apk` from the run page and install it. |
| Green | Red | The account can be licensed on GitHub, but the stock build step cannot use it yet. Fixable: send the job 2 log. |
| Red | Skipped | Account-only licensing does not work for this account. Send the job 1 log before deciding. |

If job 1 fails asking for a verification code, turn off two-step verification on the Unity ID and run it again.
