/*!@license
* Infragistics.Web.ClientUI infragistics.ext_ui.js 18.1.20181.394
*
* Copyright (c) 2011-2018 Infragistics Inc.
*
* http://www.infragistics.com/
*
* Depends:
*     jquery-1.4.4.js
*     jquery.ui.core.js
*     jquery.ui.widget.js
*     infragistics.util.js
*     infragistics.ext_core.js
*     infragistics.ext_collections.js
*/
(function(factory){if(typeof define==="function"&&define.amd){define(["./infragistics.util","./infragistics.ext_core","./infragistics.ext_collections"],factory)}else{factory(igRoot)}})(function($){$.ig=$.ig||{};var $$t={};$.ig.globalDefs=$.ig.globalDefs||{};$.ig.globalDefs.$$a=$$t;$$0=$.ig.globalDefs.$$0;$$1=$.ig.globalDefs.$$1;$$4=$.ig.globalDefs.$$4;$$6=$.ig.globalDefs.$$6;$.ig.$currDefinitions=$$t;$.ig.util.bulkDefine(["DataTemplateRenderHandler:o","DataTemplateMeasureHandler:p","DataTemplatePassHandler:q","DependencyObject:r","DependencyProperty:s","DependencyPropertiesCollection:u","DependencyPropertyChangedEventArgs:v","IDataObject:x","PropertyChangedCallback:aa","CoerceValueCallback:ab","PropertyMetadata:ac","Brush:at","LinearGradientBrush:au","CssGradientUtil:aw"]);var $a=$.ig.intDivide,$b=$.ig.util.cast,$c=$.ig.util.defType,$d=$.ig.util.defEnum,$e=$.ig.util.getBoxIfEnum,$f=$.ig.util.getDefaultValue,$g=$.ig.util.getEnumValue,$h=$.ig.util.getValue,$i=$.ig.util.intSToU,$j=$.ig.util.nullableEquals,$k=$.ig.util.nullableIsNull,$l=$.ig.util.nullableNotEquals,$m=$.ig.util.toNullable,$n=$.ig.util.toString$1,$o=$.ig.util.u32BitwiseAnd,$p=$.ig.util.u32BitwiseOr,$q=$.ig.util.u32BitwiseXor,$r=$.ig.util.u32LS,$s=$.ig.util.unwrapNullable,$t=$.ig.util.wrapNullable,$u=String.fromCharCode,$v=$.ig.util.castObjTo$t,$w=$.ig.util.compareSimple,$x=$.ig.util.tryParseNumber,$y=$.ig.util.tryParseNumber1,$z=$.ig.util.numberToString,$0=$.ig.util.numberToString1,$1=$.ig.util.parseNumber,$2=$.ig.util.compare,$3=$.ig.util.replace,$4=$.ig.util.stringFormat,$5=$.ig.util.stringFormat1,$6=$.ig.util.stringFormat2,$7=$.ig.util.stringCompare1,$8=$.ig.util.stringCompare2,$9=$.ig.util.stringCompare3;$d("ModifierKeys:aq",false,false,{None:0,Alt:1,Control:2,Shift:4,Windows:8,Apple:8});$d("Key:ap",false,false,{None:0,Back:1,Tab:2,Enter:3,Shift:4,Ctrl:5,Alt:6,CapsLock:7,Escape:8,Space:9,PageUp:10,PageDown:11,End:12,Home:13,Left:14,Up:15,Right:16,Down:17,Insert:18,"Delete:del":19,D0:20,D1:21,D2:22,D3:23,D4:24,D5:25,D6:26,D7:27,D8:28,D9:29,A:30,B:31,C:32,D:33,E:34,F:35,G:36,H:37,I:38,J:39,K:40,L:41,M:42,N:43,O:44,P:45,Q:46,R:47,S:48,T:49,U:50,V:51,W:52,X:53,Y:54,Z:55,F1:56,F2:57,F3:58,F4:59,F5:60,F6:61,F7:62,F8:63,F9:64,F10:65,F11:66,F12:67,NumPad0:68,NumPad1:69,NumPad2:70,NumPad3:71,NumPad4:72,NumPad5:73,NumPad6:74,NumPad7:75,NumPad8:76,NumPad9:77,Multiply:78,Add:79,Subtract:80,Decimal:81,Divide:82,OemSemicolon:83,OemQuestion:84,OemPipe:85,OemTilde:86,OemPlus:87,OemMinus:88,Unknown:255});$d("Stretch:bk",false,false,{None:0,Fill:1,Uniform:2,UniformToFill:3});$d("PenLineCap:bj",false,false,{Flat:0,Square:1,Round:2,Triangle:3});$d("SweepDirection:bi",false,false,{Counterclockwise:0,Clockwise:1});$d("PathSegmentType:ba",false,false,{Line:0,Bezier:1,PolyBezier:2,PolyLine:3,Arc:4});$d("GeometryType:a0",false,false,{Group:0,Line:1,Rectangle:2,Ellipse:3,Path:4});$d("FillRule:az",false,false,{EvenOdd:0,Nonzero:1});$d("Visibility:aj",false,false,{Visible:0,Collapsed:1});$d("VerticalAlignment:ai",false,false,{Top:0,Center:1,Bottom:2,Stretch:3});$d("HorizontalAlignment:w",false,false,{Left:0,Center:1,Right:2,Stretch:3});$c("APIFactory:a","Object",{init:function(){$.ig.$op.init.call(this)},createPoint:function(a,b){return{__x:a,__y:b,$type:$$t.$y.$type,getType:$.ig.$op.getType,getGetHashCode:$.ig.$op.getGetHashCode,typeName:$.ig.$op.typeName}},createRect:function(a,b,c,d){return new $$t.ae(0,a,b,c,d)},createSize:function(a,b){return new $$t.af(1,a,b)},createColor:function(a){var b=new $$t.ax;b.colorString(a);return b},$type:new $.ig.Type("APIFactory",$.ig.$ot)},true);$c("IDataObject:x","Object",{$type:new $.ig.Type("IDataObject",null)},true);$c("Clipboard:b","Object",{init:function(){$.ig.$op.init.call(this)},c:function(){return $$t.$b.b},a:function(a,b){$$t.$b.b=a},$type:new $.ig.Type("Clipboard",$.ig.$ot)},true);$c("DependencyObject:r","Object",{init:function(){$.ig.$op.init.call(this);this._localValues=new $$0.bs(0);this.a=new $$0.bs(0)},_localValues:null,a:null,c:function(a){if(this._localValues.containsKey(a.name())){return this._localValues.item(a.name())}return a.l().b()},h:function(dp_,a){if(dp_.b()){var oldValue_=null;var old=this._localValues.proxy[dp_.__name];if(typeof old!="undefined"){oldValue_=old}this._localValues.item(dp_.__name,a);dp_.l().d()(this,new $$t.v(dp_,a,oldValue_))}else{this._localValues.item(dp_.__name,a)}},f:function(a){this._localValues.remove(a.__name)},e:function(a){if(this._localValues.containsKey(a.__name)){return this._localValues.item(a.name())}return $$t.$s.c},g:function(a,b){if(a==null){return}this.a.item(a.name(),b)},getValueAlt:function(a){return this.c(a)},setValueAlt:function(dp_,a){this.h(dp_,a)},$type:new $.ig.Type("DependencyObject",$.ig.$ot)},true);$c("UIElement:c","DependencyObject",{init:function(){$$t.$r.init.call(this)},_j:null,$type:new $.ig.Type("UIElement",$$t.$r.$type)},true);$c("UIElementCollection:d","ObservableCollection$1",{ae:null,init:function(a){$$4.$e.init.call(this,$$t.$c.$type,0);this.ae=a},ac:function(a){$$4.$e.ac.call(this,a);if(a.oldItems()!=null){var c=a.oldItems().getEnumerator();while(c.moveNext()){var b=c.current();b._x=null}}if(a.newItems()!=null){var e=a.newItems().getEnumerator();while(e.moveNext()){var d=e.current();d._x=this.ae}}},p:function(){var b=this.getEnumerator();while(b.moveNext()){var a=b.current();a._x=null}$$4.$e.p.call(this)},$type:new $.ig.Type("UIElementCollection",$$4.$e.$type.specialize($$t.$c.$type))},true);$c("FrameworkElement:e","UIElement",{init:function(){this.__opacity=1;$$t.$c.init.call(this);this.__opacity=1;this._s=0;this.__visibility=0;this.width(NaN);this.height(NaN)},_name:null,name:function(a){if(arguments.length===1){this._name=a;return a}else{return this._name}},_m:0,_l:0,__visibility:0,visibility:function(a){if(arguments.length===1){if(this.__visibility!=a){var b=this.__visibility;this.__visibility=a;this.w(b,this.__visibility)}return a}else{return this.__visibility}},w:function(a,b){},_width:0,width:function(a){if(arguments.length===1){this._width=a;return a}else{return this._width}},_height:0,height:function(a){if(arguments.length===1){this._height=a;return a}else{return this._height}},_o:0,_n:0,_s:0,_x:null,_dataContext:null,dataContext:function(a){if(arguments.length===1){this._dataContext=a;return a}else{return this._dataContext}},__opacity:0,opacity:function(a){if(arguments.length===1){if(this.__opacity!=a){this.__opacity=a;this.v()}return a}else{return this.__opacity}},v:function(){},_y:null,$type:new $.ig.Type("FrameworkElement",$$t.$c.$type)},true);$c("Control:f","FrameworkElement",{init:function(){$$t.$e.init.call(this)},_ab:null,_ae:null,ac:function(){},_ad:0,_af:0,$type:new $.ig.Type("Control",$$t.$e.$type)},true);$c("ContentControl:g","Control",{init:function(){$$t.$f.init.call(this)},_content:null,content:function(a){if(arguments.length===1){this._content=a;return a}else{return this._content}},_ah:null,$type:new $.ig.Type("ContentControl",$$t.$f.$type)},true);$c("CornerRadius:h","Object",{init:function(a,b){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$.ig.$op.init.call(this);this._a=this._b=this._c=this._d=b},init1:function(a,b,c,d,e){$.ig.$op.init.call(this);this._c=b;this._d=c;this._b=d;this._a=e},_b:0,_a:0,_c:0,_d:0,$type:new $.ig.Type("CornerRadius",$.ig.$ot)},true);$c("DataFormats:i","Object",{init:function(){$.ig.$op.init.call(this)},$type:new $.ig.Type("DataFormats",$.ig.$ot)},true);$c("DataObject:j","Object",{a:null,init:function(){$.ig.$op.init.call(this);this.a=new $$4.g(String,$.ig.$op.$type,0)},getData:function(a){var $self=this;var b;if(function(){var c=$self.a.tryGetValue(a,b);b=c.p1;return c.ret}()){return b}return null},getDataPresent:function(a){return this.a.containsKey(a)},setData:function(a,b){this.a.item(a,b)},$type:new $.ig.Type("DataObject",$.ig.$ot,[$$t.$x.$type])},true);$c("DataTemplate:k","Object",{init:function(){$.ig.$op.init.call(this)},_render:null,render:function(a){if(arguments.length===1){this._render=a;return a}else{return this._render}},_measure:null,measure:function(a){if(arguments.length===1){this._measure=a;return a}else{return this._measure}},_passStarting:null,passStarting:function(a){if(arguments.length===1){this._passStarting=a;return a}else{return this._passStarting}},_passCompleted:null,passCompleted:function(a){if(arguments.length===1){this._passCompleted=a;return a}else{return this._passCompleted}},$type:new $.ig.Type("DataTemplate",$.ig.$ot)},true);$c("DataTemplatePassInfo:l","Object",{init:function(){$.ig.$op.init.call(this)},renderContext:null,context:null,viewportTop:0,viewportLeft:0,viewportWidth:0,viewportHeight:0,isHitTestRender:false,passID:null,$type:new $.ig.Type("DataTemplatePassInfo",$.ig.$ot)},true);$c("DataTemplateMeasureInfo:m","Object",{init:function(){$.ig.$op.init.call(this)},renderContext:null,context:null,width:0,height:0,isConstant:false,data:null,passInfo:null,renderOffsetX:0,renderOffsetY:0,$type:new $.ig.Type("DataTemplateMeasureInfo",$.ig.$ot)},true);$c("DataTemplateRenderInfo:n","Object",{init:function(){$.ig.$op.init.call(this)},renderContext:null,context:null,xPosition:0,yPosition:0,availableWidth:0,availableHeight:0,data:null,isHitTestRender:false,passInfo:null,renderOffsetX:0,renderOffsetY:0,$type:new $.ig.Type("DataTemplateRenderInfo",$.ig.$ot)},true);$c("UnsetValue:t","Object",{init:function(){$.ig.$op.init.call(this)},$type:new $.ig.Type("UnsetValue",$.ig.$ot)},true);$c("DependencyProperty:s","Object",{__name:null,f:null,k:null,a:false,b:function(){return this.a},init:function(a,b,c){this.a=false;$.ig.$op.init.call(this);this.__name=a;this.f=b;this.k=c;if(this.k!=null&&this.k.d()!=null){this.a=true}else{this.a=false}},l:function(){return this.k},propertyType:function(){return this.f},name:function(){return this.__name},i:function(a,b,c,d){return $$t.$u.c().e(a,b,c,d)},h:function(a,b){if(b==null){return null}var c=$$t.$u.c().d(b.typeName()+a);if(c!=null){return c}return $$t.$s.h(a,b.baseType)},registerAlt:function(a,b,c,d){return $$t.$s.i(a,b,c,d)},$type:new $.ig.Type("DependencyProperty",$.ig.$ot)},true);$c("DependencyPropertiesCollection:u","Object",{a:null,c:function(){if($$t.$u.b==null){$$t.$u.b=new $$t.u}return $$t.$u.b},init:function(){$.ig.$op.init.call(this);if(this.a==null){this.a=new $$0.bs(0)}},d:function(a){return this.a.item(a)},e:function(a,b,c,d){var e=new $$t.s(a,b,d);this.a.item(c.typeName()+a,e);return e},$type:new $.ig.Type("DependencyPropertiesCollection",$.ig.$ot)},true);$c("DependencyPropertyChangedEventArgs:v","Object",{a:null,b:null,init:function(a,b,c){$.ig.$op.init.call(this);this.a=b;this.b=c;this.e=a},e:null,f:function(a){if(arguments.length===1){this.e=a;return a}else{return this.e}},newValue:function(){return this.a},oldValue:function(){return this.b},$type:new $.ig.Type("DependencyPropertyChangedEventArgs",$.ig.$ot)},true);$c("Point:y","Object",{init:function(a){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$.ig.$op.init.call(this);this.__x=0;this.__y=0},x:function(a){if(arguments.length===1){this.__x=a;return a}else{return this.__x}},y:function(a){if(arguments.length===1){this.__y=a;return a}else{return this.__y}},__x:0,__y:0,init1:function(a,b,c){$.ig.$op.init.call(this);this.__x=b;this.__y=c},equals:function(a){if(a==null){return $.ig.$op.equals.call(this,a)}var b=a;return b.__x==this.__x&&b.__y==this.__y},getHashCode:function(){return this.__x^this.__y},l_op_Equality:function(a,b){if(a==null){return b==null}else if(b==null){return false}return a.__x==b.__x&&a.__y==b.__y},l_op_Inequality:function(a,b){return!$$t.$y.l_op_Equality(a,b)},$type:new $.ig.Type("Point",$.ig.$ot)},true);$c("PointCollection:z","List$1",{init:function(a){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$$4.$w.init.call(this,$$t.$y.$type,0)},init1:function(a,source_){$$4.$w.init.call(this,$$t.$y.$type,0);this.__inner=source_.__inner},$type:new $.ig.Type("PointCollection",$$4.$w.$type.specialize($$t.$y.$type))},true);$c("PropertyMetadata:ac","Object",{a:null,b:function(a){if(arguments.length===1){this.a=a;return a}else{return this.a}},c:null,d:function(a){if(arguments.length===1){this.c=a;return a}else{return this.c}},init:function(a,b){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break;case 2:this.init2.apply(this,arguments);break}return}$.ig.$op.init.call(this);this.b(b);this.d(null)},init1:function(a,b){$.ig.$op.init.call(this);this.b(null);this.d(b)},init2:function(a,b,c){$.ig.$op.init.call(this);this.b(b);this.d(c)},createWithCallback:function(a){return new $$t.ac(1,a)},createWithDefaultAndCallback:function(a,b){return new $$t.ac(2,a,b)},$type:new $.ig.Type("PropertyMetadata",$.ig.$ot)},true);$c("PropertyPath:ad","Object",{a:null,b:function(a){if(arguments.length===1){this.a=a;return a}else{return this.a}},init:function(a){$.ig.$op.init.call(this);this.b(a)},$type:new $.ig.Type("PropertyPath",$.ig.$ot)},true);$c("Rect:ae","Object",{init:function(a,b,c,d,e){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break;case 2:this.init2.apply(this,arguments);break;case 3:this.init3.apply(this,arguments);break;case 4:this.init4.apply(this,arguments);break}return}$.ig.$op.init.call(this);this.top(c);this.left(b);this.width(d);this.height(e)},init1:function(a,b,c,d){$.ig.$op.init.call(this);this.top(c);this.left(b);this.width(d.width());this.height(d.height())},init2:function(a,b,c){$.ig.$op.init.call(this);this.top(Math.min(b.__y,c.__y));this.left(Math.min(b.__x,c.__x));this.width(Math.max(Math.max(b.__x,c.__x)-this.left(),0));this.height(Math.max(Math.max(b.__y,c.__y)-this.top(),0))},init3:function(a,b,c){$.ig.$op.init.call(this);this.top(b.__y);this.left(b.__x);this.width(c.width());this.height(c.height())},init4:function(a){$.ig.$op.init.call(this);this.top(0);this.left(0);this.width(0);this.height(0)},s:0,x:function(a){if(arguments.length===1){this.s=a;this.o=this.s;this.p=this.o+this.r;return a}else{return this.s}},t:0,y:function(a){if(arguments.length===1){this.t=a;this.q=this.t;this.m=this.q+this.n;return a}else{return this.t}},r:0,width:function(a){if(arguments.length===1){this.r=a;this.p=this.o+this.r;return a}else{return this.r}},n:0,height:function(a){if(arguments.length===1){this.n=a;this.m=this.q+this.n;return a}else{return this.n}},q:0,top:function(a){if(arguments.length===1){this.q=a;this.y(this.q);return a}else{return this.q}},o:0,left:function(a){if(arguments.length===1){this.o=a;this.x(this.o);return a}else{return this.o}},p:0,right:function(a){if(arguments.length===1){this.p=a;this.r=this.p-this.o;return a}else{return this.p}},m:0,bottom:function(a){if(arguments.length===1){this.m=a;this.n=this.m-this.q;return a}else{return this.m}},isEmpty:function(){return this.r<0},empty:function(){return new $$t.ae(0,Number.POSITIVE_INFINITY,Number.POSITIVE_INFINITY,Number.NEGATIVE_INFINITY,Number.NEGATIVE_INFINITY)},equals1:function(a){if($$t.$ae.l_op_Equality(a,null)){return false}if(a.x()==this.x()&&a.y()==this.y()&&a.width()==this.width()&&a.height()==this.height()){return true}return false},d:function(a,b){return a>=this.s&&a-this.r<=this.s&&b>=this.t&&b-this.n<=this.t},containsLocation:function(a,b){return!this.isEmpty()&&this.d(a,b)},containsPoint:function(a){return this.containsLocation(a.__x,a.__y)},containsRect:function(a){return!this.isEmpty()&&!a.isEmpty()&&(this.s<=a.s&&this.t<=a.t&&this.s+this.r>=a.s+a.r)&&this.t+this.n>=a.t+a.n},inflate:function(a,b){this.x(this.x()-a);this.y(this.y()-b);this.width(this.width()+a*2);this.height(this.height()+b*2);if(this.r<0||this.n<0){this.af()}},af:function(){this.top(Number.POSITIVE_INFINITY);this.left(Number.POSITIVE_INFINITY);this.width(Number.NEGATIVE_INFINITY);this.height(Number.NEGATIVE_INFINITY)},intersectsWith:function(a){if(this.isEmpty()||a.isEmpty()){return false}return a.left()<this.right()&&this.left()<a.right()&&a.top()<this.bottom()&&this.top()<a.bottom()},intersect:function(a){if(!this.intersectsWith(a)){this.af()}else{var b=Math.max(this.x(),a.x());var c=Math.max(this.y(),a.y());var d=Math.min(this.x()+this.width(),a.x()+a.width())-b;var e=Math.min(this.y()+this.height(),a.y()+a.height())-c;if(d<0){d=0}if(e<0){e=0}this.r=d;this.n=e;this.s=b;this.t=c;this.o=this.s;this.q=this.t;this.p=this.o+this.r;this.m=this.q+this.n}},union:function(a){if(this.isEmpty()){this.s=a.x();this.t=a.y();this.r=a.width();this.n=a.height();this.o=this.s;this.q=this.t;this.p=this.o+this.r;this.m=this.q+this.n;return}if(!a.isEmpty()){var b=Math.min(this.x(),a.x());var c=Math.min(this.y(),a.y());var d=this.width();var e=this.height();if(a.width()==Number.POSITIVE_INFINITY||this.width()==Number.POSITIVE_INFINITY){d=Number.POSITIVE_INFINITY}else{var f=Math.max(this.right(),a.right());d=f-b}if(a.height()==Number.POSITIVE_INFINITY||this.height()==Number.POSITIVE_INFINITY){e=Number.POSITIVE_INFINITY}else{var g=Math.max(this.bottom(),a.bottom());e=g-c}this.s=b;this.t=c;this.r=d;this.n=e;this.o=this.s;this.q=this.t;this.p=this.o+this.r;this.m=this.q+this.n}},equals:function(a){if(a==null){return $.ig.$op.equals.call(this,a)}var b=a;return b.left()==this.left()&&b.top()==this.top()&&b.width()==this.width()&&b.height()==this.height()},getHashCode:function(){return this.s^this.t^this.r^this.n},copy:function(){return new $$t.ae(0,this.x(),this.y(),this.width(),this.height())},l_op_Equality:function(a,b){if(a==null){return b==null}else if(b==null){return false}return a.s==b.s&&a.t==b.t&&a.r==b.r&&a.n==b.n},l_op_Inequality:function(a,b){if(a==null){return b!=null}else if(b==null){return true}return a.s!=b.s||a.t!=b.t||a.r!=b.r||a.n!=b.n},$type:new $.ig.Type("Rect",$.ig.$ot)},true);$c("Size:af","ValueType",{init:function(a){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$$0.$bh.init.call(this)},init1:function(a,b,c){$$0.$bh.init.call(this);this.i=b;this.h=c},equals:function(a){if(a==null){return $$0.$bh.equals.call(this,a)}var b=a;return b.i==this.i&&b.h==this.h},getHashCode:function(){return this.i^this.h},i:0,h:0,width:function(a){if(arguments.length===1){this.i=a;return a}else{return this.i}},height:function(a){if(arguments.length===1){this.h=a;return a}else{return this.h}},isEmpty:function(){return this.i<0},empty:function(){var a=new $$t.af(0);a.i=Number.NEGATIVE_INFINITY;a.h=Number.NEGATIVE_INFINITY;return a},l_op_Inequality:function(a,b){return!$$t.$af.l_op_Equality(a,b)},l_op_Inequality_Lifted:function(a,b){if(!a.hasValue()){return b.hasValue()}else if(!b.hasValue()){return true}return $$t.$af.l_op_Inequality(a.value(),b.value())},l_op_Equality:function(a,b){return a.i==b.i&&a.h==b.h},l_op_Equality_Lifted:function(a,b){if(!a.hasValue()){return!b.hasValue()}else if(!b.hasValue()){return false}return $$t.$af.l_op_Equality(a.value(),b.value())},$type:new $.ig.Type("Size",$$0.$bh.$type)},true);$c("Style:ag","Object",{init:function(){this.strokeThickness=NaN;this.opacity=NaN;$.ig.$op.init.call(this)},fill:null,stroke:null,strokeThickness:0,opacity:0,$type:new $.ig.Type("Style",$.ig.$ot)},true);$c("StyleTypedPropertyAttribute","Attribute",{init:function(){$$0.$ao.init.call(this)},_a:null,_b:null,$type:new $.ig.Type("StyleTypedPropertyAttribute",$$0.$ao.$type)},true);$c("TemplatePartAttribute","Attribute",{init:function(){$$0.$ao.init.call(this)},_a:null,_b:null,$type:new $.ig.Type("TemplatePartAttribute",$$0.$ao.$type)},true);$c("TemplateVisualStateAttribute","Attribute",{init:function(){$$0.$ao.init.call(this)},_b:null,_a:null,$type:new $.ig.Type("TemplateVisualStateAttribute",$$0.$ao.$type)},true);$c("Thickness:ah","Object",{init:function(a,b){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$.ig.$op.init.call(this);this._b=this._c=this._d=this._e=b},init1:function(a,b,c,d,e){$.ig.$op.init.call(this);this._c=b;this._e=c;this._d=d;this._b=e},_b:0,_c:0,_d:0,_e:0,a:function(a){return this._b==a._b&&this._e==a._e&&this._c==a._c&&this._d==a._d},toString:function(){var a=this._c.toString()+","+this._e.toString()+","+this._d.toString()+","+this._b.toString();return a},$type:new $.ig.Type("Thickness",$.ig.$ot)},true);$c("Shape:bw","FrameworkElement",{init:function(){this.__fill=null;this.__stroke=null;$$t.$e.init.call(this)},__fill:null,fill:function(a){if(arguments.length===1){this.__fill=a;return a}else{return this.__fill}},__stroke:null,stroke:function(a){if(arguments.length===1){this.__stroke=a;return a}else{return this.__stroke}},_ab:false,_ac:0,_ai:null,_ad:0,$type:new $.ig.Type("Shape",$$t.$e.$type)},true);$c("Line:br","Shape",{init:function(){$$t.$bw.init.call(this);this._aj=0;this._ak=0;this._al=0;this._am=0},_aj:0,_ak:0,_al:0,_am:0,$type:new $.ig.Type("Line",$$t.$bw.$type)},true);$c("Path:bs","Shape",{init:function(){$$t.$bw.init.call(this)},_an:null,aj:0,ak:function(a){if(arguments.length===1){this.aj=a;return a}else{return this.aj}},al:0,am:function(a){if(arguments.length===1){this.al=a;return a}else{return this.al}},$type:new $.ig.Type("Path",$$t.$bw.$type)},true);$c("Polygon:bt","Shape",{init:function(){$$t.$bw.init.call(this);this._aj=new $$t.z(0)},_aj:null,$type:new $.ig.Type("Polygon",$$t.$bw.$type)},true);$c("Polyline:bu","Shape",{init:function(){$$t.$bw.init.call(this);this._aj=new $$t.z(0)},_aj:null,$type:new $.ig.Type("Polyline",$$t.$bw.$type)},true);$c("Rectangle:bv","Shape",{ao:null,aj:0,al:function(a){if(arguments.length===1){this.aj=a;return a}else{return this.aj}},ak:0,am:function(a){if(arguments.length===1){this.ak=a;return a}else{return this.ak}},init:function(){$$t.$bw.init.call(this);this.ao=new $$t.ae(0,0,0,0,0)},an:function(a){},$type:new $.ig.Type("Rectangle",$$t.$bw.$type)},true);$c("Brush:at","Object",{init:function(){this.__fill=null;this.f=null;this.k=new $$t.ax;$.ig.$op.init.call(this)},_isGradient:false,_isRadialGradient:false,_isImageFill:false,__fill:null,fill:function(a){if(arguments.length===1){this.__fill=a;return a}else{return this.__fill}},f:null,k:null,color:function(a){if(arguments.length===1){this.k=a;this.f=this.k.colorString();this.__fill=this.f;return a}else{if(this.__fill==null&&(this._isGradient||this._isRadialGradient)){this.__fill=this.i()}if(this.__fill==this.f){return this.k}var a=new $$t.ax;if(this.__fill!=null){a.colorString(this.__fill);this.k=a;this.f=this.__fill;if(this.__fill.length==9){this.__fill=this.k.colorString();this.f=this.__fill}}return a}},i:function(){return null},equals:function(a){if(a==null){return false}var b=a;return this.__fill.equals(b.__fill)&&this.color().equals(b.color())&&this._isGradient==b._isGradient&&this._isImageFill==b._isImageFill&&this._isRadialGradient==b._isRadialGradient},getHashCode:function(){var a=this._isGradient.getHashCode()^this._isRadialGradient.getHashCode()^this._isImageFill.getHashCode();if(this.f!=null){a^=this.f.getHashCode()}if($$t.$ax.e($m($$t.$ax.$type,this.k),$m($$t.$ax.$type,null))){a^=this.k.getHashCode()}return a},create:function(val_){$$t.$aw.touch();var b_=new $$t.at;if(!val_){return null}if(typeof val_=="string"){if($.ig.CssGradientUtil.prototype.isGradient(val_)){b_=$.ig.CssGradientUtil.prototype.brushFromGradientString(val_)}else{b_=new $.ig.Brush;b_.fill(val_)}}else if(val_.type=="linearGradient"){b_=new $.ig.LinearGradientBrush;if(val_.startPoint&&val_.endPoint){b_._useCustomDirection=true;b_._startX=val_.startPoint.x;b_._startY=val_.startPoint.y;b_._endX=val_.endPoint.x;b_._endY=val_.endPoint.y}if(val_.colorStops){stops=[];for(var i=0;i<val_.colorStops.length;i++){colorStop=new $.ig.GradientStop;colorStop._offset=val_.colorStops[i].offset;colorStop.__fill=val_.colorStops[i].color;stops.push(colorStop)}b_._gradientStops=stops}}return b_},$type:new $.ig.Type("Brush",$.ig.$ot)},true);$c("LinearGradientBrush:au","Brush",{init:function(){$$t.$at.init.call(this);this._useCustomDirection=false;this._startX=0;this._startY=0;this._endX=0;this._endY=1;this._isAbsolute=false;this._gradientStops=new Array(0);this._isGradient=true},_useCustomDirection:false,_startX:0,_startY:0,_endX:0,_endY:0,_isAbsolute:false,_gradientStops:null,clone:function(){var a=new $$t.au;a._startX=this._startX;a._startY=this._startY;a._endX=this._endX;a._endY=this._endY;a._useCustomDirection=this._useCustomDirection;a._isAbsolute=this._isAbsolute;if(this._gradientStops!=null){a._gradientStops=new Array(this._gradientStops.length);for(var b=0;b<this._gradientStops.length;b++){a._gradientStops[b]=this._gradientStops[b].clone()}}return a},equals:function(a){if(a==null){return false}var b=$b($$t.$au.$type,a);if(b==null){return false}var c=$$t.$at.equals.call(this,a)&&this._startX==b._startX&&this._startY==b._startY&&this._endX==b._endX&&this._endY==b._endY&&this._isAbsolute==b._isAbsolute&&this._useCustomDirection==b._useCustomDirection;if(c==false){return false}if(this._gradientStops.length!=b._gradientStops.length){return false}for(var d=0,e=this._gradientStops.length;d<e;d++){if(!this._gradientStops[d].equals(b._gradientStops[d])){return false}}return true},getHashCode:function(){return $$t.$at.getHashCode.call(this)^this._startX^this._startY^this._endX^this._endY},i:function(){if(this._gradientStops!=null&&this._gradientStops.length>0){return this._gradientStops[0].color().colorString()}return $$t.$at.i.call(this)},$type:new $.ig.Type("LinearGradientBrush",$$t.$at.$type)},true);$c("GradientStop:av","Object",{init:function(){this.__fill=null;this.d=null;this.g=new $$t.ax;$.ig.$op.init.call(this);this._offset=0},_offset:0,clone:function(){var a=new $$t.av;a._offset=this._offset;a.__fill=this.__fill;return a},__fill:null,fill:function(a){if(arguments.length===1){this.__fill=a;return a}else{return this.__fill}},d:null,g:null,color:function(a){if(arguments.length===1){this.g=a;this.d=this.g.colorString();this.__fill=this.d;return a}else{if(this.__fill==this.d){return this.g}var a=new $$t.ax;if(this.__fill!=null){a.colorString(this.__fill);this.g=a;this.d=this.__fill}return a}},equals:function(a){if(a==null){return false}var b=a;return this._offset==b._offset&&this.color().equals(b.color())&&this.__fill.equals(b.__fill)},getHashCode:function(){var a=this._offset;if($$t.$ax.e($m($$t.$ax.$type,this.g),$m($$t.$ax.$type,null))){a^=this.g.getHashCode()}return a},$type:new $.ig.Type("GradientStop",$.ig.$ot)},true);$c("CssGradientUtil:aw","Object",{init:function(){$.ig.$op.init.call(this)},touch:function(){},isGradient:function(a){return a.contains("linear-gradient")||a.contains("radial-gradient")},brushFromGradientString:function(a){var b=/hsl\([\s\S]+?\)[\s\S]*?[,\)]|rgba?\([\s\S]+?\)[\s\S]*?[,\)]|[^\(\)]*?[,\)]/gim,c=/\s*\d*%\s*$/,d=/^\s\s*/,e=/\s\s*$/,f=/[,\)]?$/;var g;var h,i=1,j=0,k,l;var m=false;var n;var o=a.match(b);if(o==null||o.length<=1){return null}var p=new $$t.au;k=o.length;g=o[0];if(g.contains("to")||g.contains("deg")){h=$$t.$aw.c(g);p._useCustomDirection=true;var q=$$t.$aw.a(h);p._startX=q[0].__x;p._startY=q[0].__y;p._endX=q[1].__x;p._endY=q[1].__y;n=new Array(k-1)}else{n=new Array(k);i=0}for(;i<k;i++){var r=new $$t.av;g=o[i];g=g.replace(d,"").replace(e,"").replace(f,"");l=g.search(c);if(l!=-1){r.__fill=g.substr(0,l);r._offset=parseFloat(g.substr(l+1))/100}else{r.__fill=g;r._offset=-1;m=true}n[j]=r;j++}if(m){if(n[0]._offset==-1){n[0]._offset=0}if(n[n.length-1]._offset==-1){n[n.length-1]._offset=1}$$t.$aw.f(n);p._gradientStops=n}return p},f:function(a){var b,c,d,e,f=-1,g=-1,h=0;var i=false;for(b=g+1;b<a.length;b++){var j=a[b];if(j._offset!=-1){f=Math.max(f,j._offset);j._offset=f;if(i){d=1;e=$a(f-h,b-g);for(c=g+1;c<b;c++){a[c]._offset=h+e*d;d++}i=false}g=b;h=f}else{i=true}}},c:function(a){var b=/to\s*top\s*/i,c=/to\s*right\s*top\s*/i,d=/to\s*right\s*/i,e=/to\s*right\s*bottom\s*/i,f=/to\s*bottom\s*/i,g=/to\s*left\s*bottom\s*/i,h=/to\s*left\s*/i,i=/to\s*left\s*top\s*/i;if(a.contains("deg")){return parseFloat(a)}if(b.test(a)){return 0}if(c.test(a)){return 45}if(d.test(a)){return 90}if(e.test(a)){return 135}if(f.test(a)){return 180}if(g.test(a)){return 225}if(h.test(a)){return 270}return i.test(a)?315:180},d:function(a){return Math.PI*a/180},e:function(a){if($.ig.util.isNaN(a)||Number.isInfinity(a)){return a}while(a>360){a-=360}while(a<0){a+=360}return a},a:function(a){var b=new Array(2);var c=new $$t.y(0);var d=new $$t.y(0);var e=$$t.$aw.e(a);if(e>=0&&e<=45){var f=Math.tan($$t.$aw.d(e));c.__x=.5-.5*f;c.__y=1;d.__x=.5+.5*f;d.__y=0}else if(e>180&&e<=225){var g=Math.tan($$t.$aw.d(e-180));c.__x=.5+.5*g;c.__y=0;d.__x=.5-.5*g;d.__y=1}else if(e>135&&e<=180){var h=Math.tan($$t.$aw.d(180-e));c.__x=.5-.5*h;c.__y=0;d.__x=.5+.5*h;d.__y=1}else if(e>315&&e<360){var i=Math.tan($$t.$aw.d(360-e));c.__x=.5+.5*i;c.__y=1;d.__x=.5-.5*i;d.__y=0}else if(e>45&&e<=90){var j=Math.tan($$t.$aw.d(90-e));d.__y=.5-.5*j;d.__x=1;c.__y=.5+.5*j;c.__x=0}else if(e>90&&e<=135){var k=Math.tan($$t.$aw.d(e-90));d.__y=.5+.5*k;d.__x=1;c.__y=.5-.5*k;c.__x=0}else if(e>225&&e<=270){var l=Math.tan($$t.$aw.d(270-e));c.__y=.5-.5*l;c.__x=1;d.__y=.5+.5*l;d.__x=0}else if(e>270&&e<=315){var m=Math.tan($$t.$aw.d(e-270));c.__y=.5+.5*m;c.__x=1;d.__y=.5-.5*m;d.__x=0}b[0]=c;b[1]=d;return b},$type:new $.ig.Type("CssGradientUtil",$.ig.$ot)},true);$c("Color:ax","ValueType",{init:function(){$$0.$bh.init.call(this)},__a:0,l:function(a){if(arguments.length===1){this.__a=$.ig.truncate(Math.round(a));this.a=true;return a}else{return this.__a}},__r:0,o:function(a){if(arguments.length===1){this.__r=$.ig.truncate(Math.round(a));this.a=true;return a}else{return this.__r}},__g:0,n:function(a){if(arguments.length===1){this.__g=$.ig.truncate(Math.round(a));this.a=true;return a}else{return this.__g}},__b:0,m:function(a){if(arguments.length===1){this.__b=$.ig.truncate(Math.round(a));this.a=true;return a}else{return this.__b}},__colorString:null,colorString:function(a){if(arguments.length===1){this.__colorString=a;this.r();return a}else{if(this.a||this.__colorString==null){this.a=false;this.s()}return this.__colorString}},a:false,create:function(a){if($b($$t.$ax.$type,a)!==null){return a}var b=new $$t.ax;if(typeof a==="string"){b.colorString(a)}else if(a!=null){throw new $$6.d(1,"Unknown color type")}return b},s:function(){this.__colorString="rgba("+this.__r+","+this.__g+","+this.__b+","+this.__a/255+")"},r:function(){if(this.colorString()==null){this.l(this.o(this.n(this.m(0))));return}var obj_=$.ig.util.stringToColor(this.__colorString);this.__a=typeof obj_.a!="undefined"?Math.round(obj_.a):0;this.__r=typeof obj_.r!="undefined"?Math.round(obj_.r):0;this.__g=typeof obj_.g!="undefined"?Math.round(obj_.g):0;this.__b=typeof obj_.b!="undefined"?Math.round(obj_.b):0},u:function(a_,r_,g_,b_){var a=new $$t.ax;a.__a=a_|0;a.__r=r_|0;a.__g=g_|0;a.__b=b_|0;a.a=true;return a},equals:function(a){if($b($$t.$ax.$type,a)!==null==false){return false}var b=a;return this.__a==b.__a&&this.__r==b.__r&&this.__g==b.__g&&this.__b==b.__b},getHashCode:function(){return this.__a<<24|this.__r<<16|this.__g<<8|this.__b},d:function(a,b){return!$$t.$ax.b(a,b)},e:function(a,b){if(!a.hasValue()){return b.hasValue()}else if(!b.hasValue()){return true}return $$t.$ax.d(a.value(),b.value())},b:function(a,b){return a.__a==b.__a&&a.__r==b.__r&&a.__g==b.__g&&a.__b==b.__b},c:function(a,b){if(!a.hasValue()){return!b.hasValue()}else if(!b.hasValue()){return false}return $$t.$ax.b(a.value(),b.value())},$type:new $.ig.Type("Color",$$0.$bh.$type)},true);$c("DoubleCollection:ay","List$1",{init:function(){$$4.$w.init.call(this,Number,0)},$type:new $.ig.Type("DoubleCollection",$$4.$w.$type.specialize(Number))},true);$c("Geometry:a1","Object",{init:function(){$.ig.$op.init.call(this)},a:function(){},$type:new $.ig.Type("Geometry",$.ig.$ot)},true);$c("GeometryCollection:a2","List$1",{init:function(){$$4.$w.init.call(this,$$t.$a1.$type,0)},$type:new $.ig.Type("GeometryCollection",$$4.$w.$type.specialize($$t.$a1.$type))},true);$c("GeometryGroup:a3","Geometry",{init:function(){$$t.$a1.init.call(this);this._c=new $$t.a2},_c:null,a:function(){return 0},_b:0,$type:new $.ig.Type("GeometryGroup",$$t.$a1.$type)},true);$c("LineGeometry:a4","Geometry",{init:function(){$$t.$a1.init.call(this)},_c:null,_b:null,a:function(){return 1},$type:new $.ig.Type("LineGeometry",$$t.$a1.$type)},true);$c("RectangleGeometry:a5","Geometry",{
init:function(){$$t.$a1.init.call(this)},_d:null,_b:0,_c:0,a:function(){return 2},$type:new $.ig.Type("RectangleGeometry",$$t.$a1.$type)},true);$c("EllipseGeometry:a6","Geometry",{init:function(){$$t.$a1.init.call(this)},_d:null,_b:0,_c:0,a:function(){return 3},$type:new $.ig.Type("EllipseGeometry",$$t.$a1.$type)},true);$c("PathGeometry:a7","Geometry",{init:function(){$$t.$a1.init.call(this);this._b=new $$t.a9},_b:null,a:function(){return 4},$type:new $.ig.Type("PathGeometry",$$t.$a1.$type)},true);$c("PathFigure:a8","Object",{init:function(){$.ig.$op.init.call(this);this.__segments=new $$t.bc;this.__isClosed=false;this.__isFilled=true},__segments:null,segments:function(a){if(arguments.length===1){this.__segments=a;return a}else{return this.__segments}},__startPoint:null,startPoint:function(a){if(arguments.length===1){this.__startPoint=a;return a}else{return this.__startPoint}},__isFilled:false,isFilled:function(a){if(arguments.length===1){this.__isFilled=a;return a}else{return this.__isFilled}},__isClosed:false,isClosed:function(a){if(arguments.length===1){this.__isClosed=a;return a}else{return this.__isClosed}},$type:new $.ig.Type("PathFigure",$.ig.$ot)},true);$c("PathFigureCollection:a9","List$1",{init:function(){$$4.$w.init.call(this,$$t.$a8.$type,0)},$type:new $.ig.Type("PathFigureCollection",$$4.$w.$type.specialize($$t.$a8.$type))},true);$c("PathSegment:bb","Object",{init:function(){$.ig.$op.init.call(this)},a:function(){},$type:new $.ig.Type("PathSegment",$.ig.$ot)},true);$c("PathSegmentCollection:bc","List$1",{init:function(){$$4.$w.init.call(this,$$t.$bb.$type,0)},$type:new $.ig.Type("PathSegmentCollection",$$4.$w.$type.specialize($$t.$bb.$type))},true);$c("LineSegment:bd","PathSegment",{b:null,c:function(a){if(arguments.length===1){this.b=a;return a}else{return this.b}},init:function(a,b){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$$t.$bb.init.call(this);this.c(b)},init1:function(a){$$t.$bb.init.call(this)},a:function(){return 0},$type:new $.ig.Type("LineSegment",$$t.$bb.$type)},true);$c("BezierSegment:be","PathSegment",{b:null,e:function(a){if(arguments.length===1){this.b=a;return a}else{return this.b}},c:null,f:function(a){if(arguments.length===1){this.c=a;return a}else{return this.c}},d:null,g:function(a){if(arguments.length===1){this.d=a;return a}else{return this.d}},init:function(a){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}$$t.$bb.init.call(this);this.e(this.f(this.g({__x:0,__y:0,$type:$$t.$y.$type,getType:$.ig.$op.getType,getGetHashCode:$.ig.$op.getGetHashCode,typeName:$.ig.$op.typeName})))},init1:function(a,b,c,d){$$t.$bb.init.call(this);this.e(b);this.f(c);this.g(d)},a:function(){return 1},$type:new $.ig.Type("BezierSegment",$$t.$bb.$type)},true);$c("PolyBezierSegment:bf","PathSegment",{init:function(){$$t.$bb.init.call(this);this._b=new $$t.z(0)},_b:null,a:function(){return 2},$type:new $.ig.Type("PolyBezierSegment",$$t.$bb.$type)},true);$c("PolyLineSegment:bg","PathSegment",{init:function(){$$t.$bb.init.call(this);this.__points=new $$t.z(0)},__points:null,points:function(a){if(arguments.length===1){this.__points=a;return a}else{return this.__points}},a:function(){return 3},$type:new $.ig.Type("PolyLineSegment",$$t.$bb.$type)},true);$c("ArcSegment:bh","PathSegment",{init:function(){this._f=new $$t.af;$$t.$bb.init.call(this);this._b=false;this._d=0},_e:null,_b:false,_d:0,_f:null,_c:0,a:function(){return 4},$type:new $.ig.Type("ArcSegment",$$t.$bb.$type)},true);$c("Transform:bl","DependencyObject",{init:function(){$$t.$r.init.call(this)},$type:new $.ig.Type("Transform",$$t.$r.$type)},true);$c("RotateTransform:bm","Transform",{init:function(){$$t.$bl.init.call(this)},_j:0,_k:0,_l:0,$type:new $.ig.Type("RotateTransform",$$t.$bl.$type)},true);$c("TranslateTransform:bn","Transform",{init:function(){$$t.$bl.init.call(this)},_j:0,_k:0,$type:new $.ig.Type("TranslateTransform",$$t.$bl.$type)},true);$c("ScaleTransform:bo","Transform",{init:function(){$$t.$bl.init.call(this)},_l:0,_m:0,_j:0,_k:0,$type:new $.ig.Type("ScaleTransform",$$t.$bl.$type)},true);$c("TransformGroup:bp","Transform",{_j:null,init:function(){$$t.$bl.init.call(this);this._j=new $$t.bq},$type:new $.ig.Type("TransformGroup",$$t.$bl.$type)},true);$c("TransformCollection:bq","List$1",{init:function(){$$4.$w.init.call(this,$$t.$bl.$type,0)},$type:new $.ig.Type("TransformCollection",$$4.$w.$type.specialize($$t.$bl.$type))},true);$c("MouseEventArgs:ar","EventArgs",{init:function(){$$0.$w.init.call(this)},_position:null,position:function(a){if(arguments.length===1){this._position=a;return a}else{return this._position}},_originalSource:null,originalSource:function(a){if(arguments.length===1){this._originalSource=a;return a}else{return this._originalSource}},getPosition:function(a){return this.position()},$type:new $.ig.Type("MouseEventArgs",$$0.$w.$type)},true);$c("MouseButtonEventArgs:as","MouseEventArgs",{init:function(){$$t.$ar.init.call(this)},_handled:false,handled:function(a){if(arguments.length===1){this._handled=a;return a}else{return this._handled}},$type:new $.ig.Type("MouseButtonEventArgs",$$t.$ar.$type)},true);$c("Binding:ao","Object",{init:function(a){if(a>0){switch(a){case 1:this.init1.apply(this,arguments);break}return}this.a=false;$.ig.$op.init.call(this)},init1:function(a,b){this.a=false;$.ig.$op.init.call(this);this.e=new $$t.ad(b)},c:null,d:function(a){if(arguments.length===1){this.c=a;return a}else{return this.c}},e:null,f:function(a){if(arguments.length===1){this.e=a;return a}else{return this.e}},a:false,b:function(a){if(arguments.length===1){this.a=a;return a}else{return this.a}},$type:new $.ig.Type("Binding",$.ig.$ot)},true);$c("Panel:am","FrameworkElement",{init:function(){$$t.$e.init.call(this);this._ab=new $$t.d(this)},_ab:null,$type:new $.ig.Type("Panel",$$t.$e.$type)},true);$c("Canvas:ak","Panel",{init:function(){$$t.$am.init.call(this)},$type:new $.ig.Type("Canvas",$$t.$am.$type)},true);$c("Image:al","FrameworkElement",{init:function(){$$t.$e.init.call(this)},_ac:null,_ab:false,$type:new $.ig.Type("Image",$$t.$e.$type)},true);$c("TextBlock:an","FrameworkElement",{init:function(){this.af=true;this.ae=null;this.ad=null;this.ab=null;this.ac=null;this.ah=-1;$$t.$e.init.call(this)},aj:null,ak:function(a){if(arguments.length===1){if(this.aj!=a){this.af=true;this.aj=a}return a}else{return this.aj}},_am:null,af:false,ae:null,ad:null,ab:null,ac:null,ah:0,ag:function(a,b){if(this.ah==-1){return-1}if(!this.af){if(this.ab[this.ah]==a){return this.ae[this.ah]}}for(var c=0;c<5;c++){var d=this.ah-c;if(d<0){d=5+d}if(b!=this.ac[d]||a!=this.ab[d]||this.aj!=this.ad[d]){continue}return this.ae[d]}return-1},al:function(a,b,c){if(this.ah==-1){this.ad=new Array(5);this.ab=new Array(5);this.ac=new Array(5);this.ae=new Array(5)}this.ah++;if(this.ah>5-1){this.ah=0}this.ad[this.ah]=this.aj;this.ab[this.ah]=a;this.ac[this.ah]=b;this.ae[this.ah]=c;this.af=false},$type:new $.ig.Type("TextBlock",$$t.$e.$type)},true);$$t.$b.b=null;$$t.$i.a="CSV";$$t.$i.b="HTML Format";$$t.$i.c="System.String";$$t.$i.d="Text";$$t.$i.e="UnicodeText";$$t.$s.c=new $$t.t;$$t.$u.b=null});