// Generated by `wit-bindgen` 0.11.0. DO NOT EDIT!
package the_world

// #include "the_world.h"
import "C"

type FooFooIntegersTuple2S64U8T struct {
  F0 int64
  F1 uint8
}

// Import functions from foo:foo/integers
func FooFooIntegersA1(x uint8) {
  lower_x := C.uint8_t(x)
  C.foo_foo_integers_a1(lower_x)
}

func FooFooIntegersA2(x int8) {
  lower_x := C.int8_t(x)
  C.foo_foo_integers_a2(lower_x)
}

func FooFooIntegersA3(x uint16) {
  lower_x := C.uint16_t(x)
  C.foo_foo_integers_a3(lower_x)
}

func FooFooIntegersA4(x int16) {
  lower_x := C.int16_t(x)
  C.foo_foo_integers_a4(lower_x)
}

func FooFooIntegersA5(x uint32) {
  lower_x := C.uint32_t(x)
  C.foo_foo_integers_a5(lower_x)
}

func FooFooIntegersA6(x int32) {
  lower_x := C.int32_t(x)
  C.foo_foo_integers_a6(lower_x)
}

func FooFooIntegersA7(x uint64) {
  lower_x := C.uint64_t(x)
  C.foo_foo_integers_a7(lower_x)
}

func FooFooIntegersA8(x int64) {
  lower_x := C.int64_t(x)
  C.foo_foo_integers_a8(lower_x)
}

func FooFooIntegersA9(p1 uint8, p2 int8, p3 uint16, p4 int16, p5 uint32, p6 int32, p7 uint64, p8 int64) {
  lower_p1 := C.uint8_t(p1)
  lower_p2 := C.int8_t(p2)
  lower_p3 := C.uint16_t(p3)
  lower_p4 := C.int16_t(p4)
  lower_p5 := C.uint32_t(p5)
  lower_p6 := C.int32_t(p6)
  lower_p7 := C.uint64_t(p7)
  lower_p8 := C.int64_t(p8)
  C.foo_foo_integers_a9(lower_p1, lower_p2, lower_p3, lower_p4, lower_p5, lower_p6, lower_p7, lower_p8)
}

func FooFooIntegersR1() uint8 {
  ret := C.foo_foo_integers_r1()
  var lift_ret uint8
  lift_ret = uint8(ret)
  return lift_ret
}

func FooFooIntegersR2() int8 {
  ret := C.foo_foo_integers_r2()
  var lift_ret int8
  lift_ret = int8(ret)
  return lift_ret
}

func FooFooIntegersR3() uint16 {
  ret := C.foo_foo_integers_r3()
  var lift_ret uint16
  lift_ret = uint16(ret)
  return lift_ret
}

func FooFooIntegersR4() int16 {
  ret := C.foo_foo_integers_r4()
  var lift_ret int16
  lift_ret = int16(ret)
  return lift_ret
}

func FooFooIntegersR5() uint32 {
  ret := C.foo_foo_integers_r5()
  var lift_ret uint32
  lift_ret = uint32(ret)
  return lift_ret
}

func FooFooIntegersR6() int32 {
  ret := C.foo_foo_integers_r6()
  var lift_ret int32
  lift_ret = int32(ret)
  return lift_ret
}

func FooFooIntegersR7() uint64 {
  ret := C.foo_foo_integers_r7()
  var lift_ret uint64
  lift_ret = uint64(ret)
  return lift_ret
}

func FooFooIntegersR8() int64 {
  ret := C.foo_foo_integers_r8()
  var lift_ret int64
  lift_ret = int64(ret)
  return lift_ret
}

func FooFooIntegersPairRet() FooFooIntegersTuple2S64U8T {
  var ret C.the_world_tuple2_s64_u8_t
  C.foo_foo_integers_pair_ret(&ret)
  var lift_ret FooFooIntegersTuple2S64U8T
  var lift_ret_F0 int64
  lift_ret_F0 = int64(ret.f0)
  lift_ret.F0 = lift_ret_F0
  var lift_ret_F1 uint8
  lift_ret_F1 = uint8(ret.f1)
  lift_ret.F1 = lift_ret_F1
  return lift_ret
}

// Export functions from foo:foo/integers
var foo_foo_integers ExportsFooFooIntegers = nil
func SetExportsFooFooIntegers(i ExportsFooFooIntegers) {
  foo_foo_integers = i
}
type ExportsFooFooIntegers interface {
  A1(x uint8) 
  A2(x int8) 
  A3(x uint16) 
  A4(x int16) 
  A5(x uint32) 
  A6(x int32) 
  A7(x uint64) 
  A8(x int64) 
  A9(p1 uint8, p2 int8, p3 uint16, p4 int16, p5 uint32, p6 int32, p7 uint64, p8 int64) 
  R1() uint8 
  R2() int8 
  R3() uint16 
  R4() int16 
  R5() uint32 
  R6() int32 
  R7() uint64 
  R8() int64 
  PairRet() FooFooIntegersTuple2S64U8T 
}
//export exports_foo_foo_integers_a1
func ExportsFooFooIntegersA1(x C.uint8_t) {
  var lift_x uint8
  lift_x = uint8(x)
  foo_foo_integers.A1(lift_x)
  
}
//export exports_foo_foo_integers_a2
func ExportsFooFooIntegersA2(x C.int8_t) {
  var lift_x int8
  lift_x = int8(x)
  foo_foo_integers.A2(lift_x)
  
}
//export exports_foo_foo_integers_a3
func ExportsFooFooIntegersA3(x C.uint16_t) {
  var lift_x uint16
  lift_x = uint16(x)
  foo_foo_integers.A3(lift_x)
  
}
//export exports_foo_foo_integers_a4
func ExportsFooFooIntegersA4(x C.int16_t) {
  var lift_x int16
  lift_x = int16(x)
  foo_foo_integers.A4(lift_x)
  
}
//export exports_foo_foo_integers_a5
func ExportsFooFooIntegersA5(x C.uint32_t) {
  var lift_x uint32
  lift_x = uint32(x)
  foo_foo_integers.A5(lift_x)
  
}
//export exports_foo_foo_integers_a6
func ExportsFooFooIntegersA6(x C.int32_t) {
  var lift_x int32
  lift_x = int32(x)
  foo_foo_integers.A6(lift_x)
  
}
//export exports_foo_foo_integers_a7
func ExportsFooFooIntegersA7(x C.uint64_t) {
  var lift_x uint64
  lift_x = uint64(x)
  foo_foo_integers.A7(lift_x)
  
}
//export exports_foo_foo_integers_a8
func ExportsFooFooIntegersA8(x C.int64_t) {
  var lift_x int64
  lift_x = int64(x)
  foo_foo_integers.A8(lift_x)
  
}
//export exports_foo_foo_integers_a9
func ExportsFooFooIntegersA9(p1 C.uint8_t, p2 C.int8_t, p3 C.uint16_t, p4 C.int16_t, p5 C.uint32_t, p6 C.int32_t, p7 C.uint64_t, p8 C.int64_t) {
  var lift_p1 uint8
  lift_p1 = uint8(p1)
  var lift_p2 int8
  lift_p2 = int8(p2)
  var lift_p3 uint16
  lift_p3 = uint16(p3)
  var lift_p4 int16
  lift_p4 = int16(p4)
  var lift_p5 uint32
  lift_p5 = uint32(p5)
  var lift_p6 int32
  lift_p6 = int32(p6)
  var lift_p7 uint64
  lift_p7 = uint64(p7)
  var lift_p8 int64
  lift_p8 = int64(p8)
  foo_foo_integers.A9(lift_p1, lift_p2, lift_p3, lift_p4, lift_p5, lift_p6, lift_p7, lift_p8)
  
}
//export exports_foo_foo_integers_r1
func ExportsFooFooIntegersR1() C.uint8_t {
  result := foo_foo_integers.R1()
  lower_result := C.uint8_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r2
func ExportsFooFooIntegersR2() C.int8_t {
  result := foo_foo_integers.R2()
  lower_result := C.int8_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r3
func ExportsFooFooIntegersR3() C.uint16_t {
  result := foo_foo_integers.R3()
  lower_result := C.uint16_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r4
func ExportsFooFooIntegersR4() C.int16_t {
  result := foo_foo_integers.R4()
  lower_result := C.int16_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r5
func ExportsFooFooIntegersR5() C.uint32_t {
  result := foo_foo_integers.R5()
  lower_result := C.uint32_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r6
func ExportsFooFooIntegersR6() C.int32_t {
  result := foo_foo_integers.R6()
  lower_result := C.int32_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r7
func ExportsFooFooIntegersR7() C.uint64_t {
  result := foo_foo_integers.R7()
  lower_result := C.uint64_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_r8
func ExportsFooFooIntegersR8() C.int64_t {
  result := foo_foo_integers.R8()
  lower_result := C.int64_t(result)
  return lower_result
  
}
//export exports_foo_foo_integers_pair_ret
func ExportsFooFooIntegersPairRet(ret *C.the_world_tuple2_s64_u8_t) {
  result := foo_foo_integers.PairRet()
  var lower_result C.the_world_tuple2_s64_u8_t
  lower_result_f0 := C.int64_t(result.F0)
  lower_result.f0 = lower_result_f0
  lower_result_f1 := C.uint8_t(result.F1)
  lower_result.f1 = lower_result_f1
  *ret = lower_result
  
}
