# Crosswalk Extensions for "two-in-one" hybrid Laptop/Tablet Devices

Crosswalk extensions to support laptops with detachable or foldable keyboards. Currently only Windows versions 8.x and 10 are supported.

## Build and run demo from source

Required dependencies are Microsoft Visual Studio 2015 and Git for Windows.

* Create a directory to hold everything. We will refer to the *absolute path* of this directory using the ``<crosswalk>`` placeholder in the future.

```
> mkdir crosswalk
> cd crosswalk
```

* Download crosswalk from https://download.01.org/crosswalk/releases/crosswalk/windows/beta/latest/ and unzip it, e.g. crosswalk64-19.49.514.4.zip. Make sure not to end up with nested directories after unpacking.

```
> dir

 Directory of <crosswalk>

22/03/2016  15:02    <DIR>          crosswalk64-19.49.514.4
22/03/2016  14:38        37,911,343 crosswalk64-19.49.514.4.zip
```

* Clone the repository.

```
> git clone https://github.com/crosswalk-project/crosswalk-extensions-twoinone.git
> cd crosswalk-extensions-twoinone
```

* Open ``twoinone-windows\twoinone.sln`` in Visual Studio 2015 and build the solution.

* Enter demo dir and run the demo. Replace ``<crosswalk>`` with the absolute path to the newly created root directory for this crosswalk extension from step 1.

```
> cd demo-posture
> ..\..\crosswalk64-19.49.514.4\xwalk.exe --external-extensions-path=<crosswalk>\crosswalk-extensions-twoinone\twoinone-windows\bin\Debug --enable-inspector --enable-logging -v=1 app\manifest.json
```
