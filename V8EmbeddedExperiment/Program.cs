using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V8.Net;

namespace TestReturnValue
{

    public class CompletionEntry1 : V8NativeObject
    {
        public string name { get { return GetProperty("name"); } set { SetProperty("name", value); } }
        public string kind { get { return GetProperty("kind"); } set { SetProperty("kind", value); } }
        public string kindModifiers { get { return GetProperty("kindModifiers"); } set { SetProperty("kindModifiers", value); } }
    }

    public class CompletionInfo1 : V8NativeObject
    {
        public bool isMemberCompletion { get { return GetProperty("isMemberCompletion").AsBoolean; } set { SetProperty("isMemberCompletion", value); } }
        public CompletionEntry1[] entries;
    }
    public class CompletionEntry2 // NOTE: Inheriting from V8NativeObject not required here (though still allowed).
    {
        public string name { get; set; }
        public string kind { get; set; }
        public string kindModifiers { get; set; }
    }

    public class CompletionInfo2 // NOTE: Inheriting from V8NativeObject not required here (though still allowed).
    {
        public bool isMemberCompletion { get; set; }
        public CompletionEntry2[] entries;

        public CompletionInfo2(string a, string b, string c)
        {
            isMemberCompletion = true;
            entries = new CompletionEntry2[3] {
                new CompletionEntry2 {
                    name = "start_"+a,
                    kind = "method",
                    kindModifiers = "public"
                },
                new CompletionEntry2 {
                    name ="drive_"+b,
                    kind ="method",
                    kindModifiers = "public"
                },
                new CompletionEntry2 {
                    name = "getPosition",
                    kind = "method_"+c,
                    kindModifiers = "public"
                }
            };
        }

        public InternalHandle AddOne(int i, V8Engine engine) // JS call example: "new CompletionInfo2().AddOne(1) // returns 2"
            => engine.CreateValue(i + 1);
        // (the binding system will pass in the engine automatically; InternalHandle parameter types also allowed)
    }

    class Program
    {
        static void Main(string[] args)
        {
            V8Engine v8Engine = new V8Engine();

            var result = v8Engine.Execute(
                @"var TypescriptService = (function () {
                    function TypescriptService() {
                    }
                    TypescriptService.prototype.getCompletionsAtPosition = function (a, b, c) {
                        var entries = [
                            {
                                'name': 'start_'+a,
                                'kind': 'method',
                                'kindModifiers': 'public'
                            },
                            {
                                'name': 'drive_'+b,
                                'kind': 'method',
                                'kindModifiers': 'public'
                            },
                            {
                                'name': 'getPosition',
                                'kind': 'method_'+c,
                                'kindModifiers': 'public'
                            }
                        ];
                        var result = {
                            'isMemberCompletion': true,
                            'entries': entries
                        };
                        return result;
                    };
                    return TypescriptService;
                })();
                var ls = new TypescriptService();" // NOTE: 'result' WILL ALWAYS BE "undefined" BECAUSE OF "var ..."
            );

            //create parameter
            Handle filename = v8Engine.CreateValue("a");
            Handle position = v8Engine.CreateValue("b");
            Handle isMemberCompletion = v8Engine.CreateValue("c");

            var ls = v8Engine.GlobalObject.GetProperty("ls");
            var resultHandle = ls.Call("getCompletionsAtPosition", null, filename, position, isMemberCompletion); // NOTE: The object context is already known, so pass 'null' for '_this'.
            CompletionInfo1 completion = v8Engine.GetObject<CompletionInfo1>(resultHandle);

            //examine result
            var test0 = resultHandle.GetProperty("isMemberCompletion");

            var test1 = resultHandle.GetProperty("entries"); // NOTE: "ObjectHandle" is a special handle for objects (which also obviously includes arrays, etc.).
            var arrayLength = test1.ArrayLength;
            var arrayItem1 = test1.GetProperty(0);
            var arrayItem1_name = arrayItem1.GetProperty("name");
            var arrayItem1_kind = arrayItem1.GetProperty("kind");
            var arrayItem1_kindModifiers = arrayItem1.GetProperty("kindModifiers");
            var arrayItem2 = test1.GetProperty(1); // (arrays are treated same as objects here)
            var arrayItem3 = test1.GetProperty(2); // (arrays are treated same as objects here)

            //  ==================================================================== OR  ====================================================================

            v8Engine.RegisterType<CompletionInfo2>(null, true, ScriptMemberSecurity.Locked);

            v8Engine.GlobalObject.SetProperty(typeof(CompletionInfo2)); // <= THIS IS IMPORTANT! It sets the type on the global object (though you can put this anywhere like any property)

            var ls2 = (InternalHandle)v8Engine.Execute(
                @"var TypescriptService = (function () {
                    function TypescriptService() {
                    }
                    TypescriptService.prototype.getCompletionsAtPosition = function (a, b, c) {
                        return new CompletionInfo2(a, b, c);
                    };
                    return TypescriptService;
                })();
                new TypescriptService();" // NOTE: result2 is not a handle to the service.
            );

            var resultHandle2 = ls2.Call("getCompletionsAtPosition", null, filename, position, isMemberCompletion); // NOTE: The object context is already known, so pass 'null' for '_this'.
            var objectBindingModeIfNeeded = resultHandle2.BindingMode;
            var ce2 = (CompletionInfo2)resultHandle2.BoundObject; // (when a CLR class is bound, it is tracked by the handle in a special way)

            //  =============================================================================================================================================
            // Take your pick. ;)
        }
    }
}