# Elegant.Flow

React inspired component based UI for unity3d.

This is a pre-release version, as such there are no tagged releases.

## Usage

See the 'tests' folder for samples.

## Install

From your unity project folder:

    npm init
    npm install shadowmint/unity-elegant-flow --save
    echo Assets/packages >> .gitignore
    echo Assets/packages.meta >> .gitignore

The package and all its dependencies will be installed in
your Assets/packages folder.

## Development

Setup and run tests:

    npm install
    cd test
    npm install ..

Remember that changes made to the test folder are not saved to the package
unless they are copied back into the source folder.

To reinstall the files from the src folder, run `npm install ..` again.

## Tests

All tests and samples are inside the 'tests' folder.
