using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PrimeiraTelaWinUI.Data;
using WinRT;
using WinRT.Interop;
using WinRT.PrimeiraTelaWinUIVtableClasses;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;

namespace PrimeiraTelaWinUI.Views;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPageWinRTTypeDetails))]
public sealed class ProjectDetailsPage : Page, IComponentConnector
{
	private sealed class Q1ParsedRow
	{
		public string Number { get; }

		public string Cause { get; }

		public string Value1 { get; }

		public string Value2 { get; }

		public string Value3 { get; }

		public string Value4 { get; }

		public string Value5 { get; }

		public double Count1 { get; }

		public double Count2 { get; }

		public double Count3 { get; }

		public double Count4 { get; }

		public double Count5 { get; }

		public Q1ParsedRow(string number, string cause, string value1, string value2, string value3, string value4, string value5, double count1, double count2, double count3, double count4, double count5)
		{
			Number = number;
			Cause = cause;
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
			Value5 = value5;
			Count1 = count1;
			Count2 = count2;
			Count3 = count3;
			Count4 = count4;
			Count5 = count5;
		}
	}

	private sealed class TopsisAlternativeData
	{
		public string Id { get; }

		public string Cause { get; }

		public double[] CriteriaValues { get; }

		public TopsisAlternativeData(string id, string cause, double[] criterionValues)
		{
			Id = id;
			Cause = cause;
			CriteriaValues = criterionValues;
		}
	}

	private sealed class AhpComputationResult
	{
		public IReadOnlyList<double> Weights { get; }

		public double CrPercent { get; }

		public int ResponseCount { get; }

		public AhpComputationResult(IReadOnlyList<double> weights, double crPercent, int responseCount)
		{
			Weights = weights;
			CrPercent = crPercent;
			ResponseCount = responseCount;
		}
	}

	private struct AhpPairDefinition
	{
		public int GroupAIndex { get; }

		public int GroupBIndex { get; }

		public int PairNumber { get; }

		public int ColumnA { get; set; }

		public int ColumnB { get; set; }

		public AhpPairDefinition(int groupAIndex, int groupBIndex, int pairNumber)
		{
			GroupAIndex = groupAIndex;
			GroupBIndex = groupBIndex;
			PairNumber = pairNumber;
			ColumnA = -1;
			ColumnB = -1;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private interface IProjectDetailsPage_Bindings
	{
		void Initialize();

		void Update();

		void StopTracking();

		void DisconnectUnloadedObject(int connectionId);
	}

	private interface IProjectDetailsPage_BindingsScopeConnector
	{
		WeakReference Parent { get; set; }

		bool ContainsElement(int connectionId);

		void RegisterForElementConnection(int connectionId, IComponentConnector connector);
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	private static class XamlBindingSetters
	{
		public static void Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(TextBlock obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			obj.Text = value ?? string.Empty;
		}

		public static void Set_Microsoft_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(ToggleButton obj, bool? value, string targetNullValue)
		{
			if (!value.HasValue && targetNullValue != null)
			{
				value = (bool)XamlBindingHelper.ConvertValue(typeof(bool), targetNullValue);
			}
			obj.IsChecked = value;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj7_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		private TopsisTableRow dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj7;

		private TextBlock obj8;

		private TextBlock obj9;

		private TextBlock obj10;

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 7:
				obj7 = new WeakReference(target.As<Grid>());
				break;
			case 8:
				obj8 = target.As<TextBlock>();
				break;
			case 9:
				obj9 = target.As<TextBlock>();
				break;
			case 10:
				obj10 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj7.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<TopsisTableRow>(), 1 << phase);
		}

		public void Recycle()
		{
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<TopsisTableRow>();
				return true;
			}
			return false;
		}

		private void Update_(TopsisTableRow obj, int phase)
		{
			if (obj != null && (phase & -2147483647) != 0)
			{
				Update_Id(obj.Id, phase);
				Update_Cause(obj.Cause, phase);
				Update_Rank(obj.Rank, phase);
			}
		}

		private void Update_Id(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj8, obj, null);
			}
		}

		private void Update_Cause(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj9, obj, null);
			}
		}

		private void Update_Rank(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj10, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj23_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		private TopsisTableRow dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj23;

		private TextBlock obj24;

		private TextBlock obj25;

		private TextBlock obj26;

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 23:
				obj23 = new WeakReference(target.As<Grid>());
				break;
			case 24:
				obj24 = target.As<TextBlock>();
				break;
			case 25:
				obj25 = target.As<TextBlock>();
				break;
			case 26:
				obj26 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj23.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<TopsisTableRow>(), 1 << phase);
		}

		public void Recycle()
		{
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<TopsisTableRow>();
				return true;
			}
			return false;
		}

		private void Update_(TopsisTableRow obj, int phase)
		{
			if (obj != null && (phase & -2147483647) != 0)
			{
				Update_Id(obj.Id, phase);
				Update_Cause(obj.Cause, phase);
				Update_Rank(obj.Rank, phase);
			}
		}

		private void Update_Id(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj24, obj, null);
			}
		}

		private void Update_Cause(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj25, obj, null);
			}
		}

		private void Update_Rank(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj26, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj30_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		private AhpTableRow dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj30;

		private TextBlock obj31;

		private TextBlock obj32;

		private TextBlock obj33;

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 30:
				obj30 = new WeakReference(target.As<Grid>());
				break;
			case 31:
				obj31 = target.As<TextBlock>();
				break;
			case 32:
				obj32 = target.As<TextBlock>();
				break;
			case 33:
				obj33 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj30.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<AhpTableRow>(), 1 << phase);
		}

		public void Recycle()
		{
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<AhpTableRow>();
				return true;
			}
			return false;
		}

		private void Update_(AhpTableRow obj, int phase)
		{
			if (obj != null && (phase & -2147483647) != 0)
			{
				Update_Id(obj.Id, phase);
				Update_Group(obj.Group, phase);
				Update_Weight(obj.Weight, phase);
			}
		}

		private void Update_Id(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj31, obj, null);
			}
		}

		private void Update_Group(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj32, obj, null);
			}
		}

		private void Update_Weight(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj33, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj42_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		private Q1TableRow dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj42;

		private TextBlock obj43;

		private TextBlock obj44;

		private TextBlock obj45;

		private TextBlock obj46;

		private TextBlock obj47;

		private TextBlock obj48;

		private TextBlock obj49;

		private TextBlock obj50;

		private TextBlock obj51;

		private TextBlock obj52;

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 42:
				obj42 = new WeakReference(target.As<Grid>());
				break;
			case 43:
				obj43 = target.As<TextBlock>();
				break;
			case 44:
				obj44 = target.As<TextBlock>();
				break;
			case 45:
				obj45 = target.As<TextBlock>();
				break;
			case 46:
				obj46 = target.As<TextBlock>();
				break;
			case 47:
				obj47 = target.As<TextBlock>();
				break;
			case 48:
				obj48 = target.As<TextBlock>();
				break;
			case 49:
				obj49 = target.As<TextBlock>();
				break;
			case 50:
				obj50 = target.As<TextBlock>();
				break;
			case 51:
				obj51 = target.As<TextBlock>();
				break;
			case 52:
				obj52 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj42.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<Q1TableRow>(), 1 << phase);
		}

		public void Recycle()
		{
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<Q1TableRow>();
				return true;
			}
			return false;
		}

		private void Update_(Q1TableRow obj, int phase)
		{
			if (obj != null && (phase & -2147483647) != 0)
			{
				Update_Number(obj.Number, phase);
				Update_Cause(obj.Cause, phase);
				Update_Value1(obj.Value1, phase);
				Update_Value2(obj.Value2, phase);
				Update_Value3(obj.Value3, phase);
				Update_Value4(obj.Value4, phase);
				Update_Value5(obj.Value5, phase);
				Update_Median(obj.Median, phase);
				Update_Proportion(obj.Proportion, phase);
				Update_Status(obj.Status, phase);
			}
		}

		private void Update_Number(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj43, obj, null);
			}
		}

		private void Update_Cause(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj44, obj, null);
			}
		}

		private void Update_Value1(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj45, obj, null);
			}
		}

		private void Update_Value2(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj46, obj, null);
			}
		}

		private void Update_Value3(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj47, obj, null);
			}
		}

		private void Update_Value4(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj48, obj, null);
			}
		}

		private void Update_Value5(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj49, obj, null);
			}
		}

		private void Update_Median(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj50, obj, null);
			}
		}

		private void Update_Proportion(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj51, obj, null);
			}
		}

		private void Update_Status(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj52, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj65_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		private class ProjectDetailsPage_obj65_BindingsTracking
		{
			private WeakReference<ProjectDetailsPage_obj65_Bindings> weakRefToBindingObj;

			public ProjectDetailsPage_obj65_BindingsTracking(ProjectDetailsPage_obj65_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<ProjectDetailsPage_obj65_Bindings>(obj);
			}

			public ProjectDetailsPage_obj65_Bindings TryGetBindingObject()
			{
				ProjectDetailsPage_obj65_Bindings bindingObject = null;
				if (weakRefToBindingObj != null)
				{
					weakRefToBindingObj.TryGetTarget(out bindingObject);
					if (bindingObject == null)
					{
						weakRefToBindingObj = null;
						ReleaseAllListeners();
					}
				}
				return bindingObject;
			}

			public void ReleaseAllListeners()
			{
				UpdateChildListeners_(null);
			}

			public void PropertyChanged_(object sender, PropertyChangedEventArgs e)
			{
				ProjectDetailsPage_obj65_Bindings bindings = TryGetBindingObject();
				if (bindings == null)
				{
					return;
				}
				string propName = e.PropertyName;
				NamedListItem obj = sender as NamedListItem;
				if (string.IsNullOrEmpty(propName))
				{
					if (obj != null)
					{
						bindings.Update_ItemId(obj.ItemId, 1073741824);
						bindings.Update_Name(obj.Name, 1073741824);
					}
				}
				else if (!(propName == "ItemId"))
				{
					if (propName == "Name" && obj != null)
					{
						bindings.Update_Name(obj.Name, 1073741824);
					}
				}
				else if (obj != null)
				{
					bindings.Update_ItemId(obj.ItemId, 1073741824);
				}
			}

			public void UpdateChildListeners_(NamedListItem obj)
			{
				ProjectDetailsPage_obj65_Bindings bindings = TryGetBindingObject();
				if (bindings != null)
				{
					if (bindings.dataRoot != null)
					{
						((INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
					}
					if (obj != null)
					{
						bindings.dataRoot = obj;
						((INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
					}
				}
			}
		}

		private NamedListItem dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj65;

		private TextBlock obj66;

		private TextBlock obj67;

		private ProjectDetailsPage_obj65_BindingsTracking bindingsTracking;

		public ProjectDetailsPage_obj65_Bindings()
		{
			bindingsTracking = new ProjectDetailsPage_obj65_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 65:
				obj65 = new WeakReference(target.As<Grid>());
				break;
			case 66:
				obj66 = target.As<TextBlock>();
				break;
			case 67:
				obj67 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj65.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<NamedListItem>(), 1 << phase);
		}

		public void Recycle()
		{
			bindingsTracking.ReleaseAllListeners();
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
			bindingsTracking.ReleaseAllListeners();
			initialized = false;
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			bindingsTracking.ReleaseAllListeners();
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<NamedListItem>();
				return true;
			}
			return false;
		}

		private void Update_(NamedListItem obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_(obj);
			if (obj != null && (phase & -1073741823) != 0)
			{
				Update_ItemId(obj.ItemId, phase);
				Update_Name(obj.Name, phase);
			}
		}

		private void Update_ItemId(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj66, obj, null);
			}
		}

		private void Update_Name(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj67, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj71_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		private class ProjectDetailsPage_obj71_BindingsTracking
		{
			private WeakReference<ProjectDetailsPage_obj71_Bindings> weakRefToBindingObj;

			public ProjectDetailsPage_obj71_BindingsTracking(ProjectDetailsPage_obj71_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<ProjectDetailsPage_obj71_Bindings>(obj);
			}

			public ProjectDetailsPage_obj71_Bindings TryGetBindingObject()
			{
				ProjectDetailsPage_obj71_Bindings bindingObject = null;
				if (weakRefToBindingObj != null)
				{
					weakRefToBindingObj.TryGetTarget(out bindingObject);
					if (bindingObject == null)
					{
						weakRefToBindingObj = null;
						ReleaseAllListeners();
					}
				}
				return bindingObject;
			}

			public void ReleaseAllListeners()
			{
				UpdateChildListeners_(null);
			}

			public void PropertyChanged_(object sender, PropertyChangedEventArgs e)
			{
				ProjectDetailsPage_obj71_Bindings bindings = TryGetBindingObject();
				if (bindings == null)
				{
					return;
				}
				string propName = e.PropertyName;
				GroupSelectionOption obj = sender as GroupSelectionOption;
				if (string.IsNullOrEmpty(propName))
				{
					if (obj != null)
					{
						bindings.Update_IsSelected(obj.IsSelected, 1073741824);
						bindings.Update_GroupName(obj.GroupName, 1073741824);
					}
				}
				else if (!(propName == "IsSelected"))
				{
					if (propName == "GroupName" && obj != null)
					{
						bindings.Update_GroupName(obj.GroupName, 1073741824);
					}
				}
				else if (obj != null)
				{
					bindings.Update_IsSelected(obj.IsSelected, 1073741824);
				}
			}

			public void UpdateChildListeners_(GroupSelectionOption obj)
			{
				ProjectDetailsPage_obj71_Bindings bindings = TryGetBindingObject();
				if (bindings != null)
				{
					if (bindings.dataRoot != null)
					{
						((INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
					}
					if (obj != null)
					{
						bindings.dataRoot = obj;
						((INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
					}
				}
			}

			public void RegisterTwoWayListener_72(CheckBox sourceObject)
			{
				sourceObject.RegisterPropertyChangedCallback(ToggleButton.IsCheckedProperty, delegate
				{
					TryGetBindingObject()?.UpdateTwoWay_72_IsChecked();
				});
			}
		}

		private GroupSelectionOption dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj71;

		private CheckBox obj72;

		private TextBlock obj73;

		private ProjectDetailsPage_obj71_BindingsTracking bindingsTracking;

		public ProjectDetailsPage_obj71_Bindings()
		{
			bindingsTracking = new ProjectDetailsPage_obj71_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 71:
				obj71 = new WeakReference(target.As<StackPanel>());
				break;
			case 72:
				obj72 = target.As<CheckBox>();
				bindingsTracking.RegisterTwoWayListener_72(obj72);
				break;
			case 73:
				obj73 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					StackPanel rootElement = obj71.Target as StackPanel;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<GroupSelectionOption>(), 1 << phase);
		}

		public void Recycle()
		{
			bindingsTracking.ReleaseAllListeners();
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
			bindingsTracking.ReleaseAllListeners();
			initialized = false;
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			bindingsTracking.ReleaseAllListeners();
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<GroupSelectionOption>();
				return true;
			}
			return false;
		}

		private void Update_(GroupSelectionOption obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_(obj);
			if (obj != null && (phase & -1073741823) != 0)
			{
				Update_IsSelected(obj.IsSelected, phase);
				Update_GroupName(obj.GroupName, phase);
			}
		}

		private void Update_IsSelected(bool obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(obj72, obj, null);
			}
		}

		private void Update_GroupName(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj73, obj, null);
			}
		}

		private void UpdateTwoWay_72_IsChecked()
		{
			if (initialized && dataRoot != null)
			{
				dataRoot.IsSelected = obj72.IsChecked.Value;
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(PrimeiraTelaWinUI_Views_ProjectDetailsPage_ProjectDetailsPage_obj30_BindingsWinRTTypeDetails))]
	private class ProjectDetailsPage_obj83_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, IProjectDetailsPage_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		private class ProjectDetailsPage_obj83_BindingsTracking
		{
			private WeakReference<ProjectDetailsPage_obj83_Bindings> weakRefToBindingObj;

			public ProjectDetailsPage_obj83_BindingsTracking(ProjectDetailsPage_obj83_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<ProjectDetailsPage_obj83_Bindings>(obj);
			}

			public ProjectDetailsPage_obj83_Bindings TryGetBindingObject()
			{
				ProjectDetailsPage_obj83_Bindings bindingObject = null;
				if (weakRefToBindingObj != null)
				{
					weakRefToBindingObj.TryGetTarget(out bindingObject);
					if (bindingObject == null)
					{
						weakRefToBindingObj = null;
						ReleaseAllListeners();
					}
				}
				return bindingObject;
			}

			public void ReleaseAllListeners()
			{
				UpdateChildListeners_(null);
			}

			public void PropertyChanged_(object sender, PropertyChangedEventArgs e)
			{
				ProjectDetailsPage_obj83_Bindings bindings = TryGetBindingObject();
				if (bindings == null)
				{
					return;
				}
				string propName = e.PropertyName;
				NamedListItem obj = sender as NamedListItem;
				if (string.IsNullOrEmpty(propName))
				{
					if (obj != null)
					{
						bindings.Update_ItemId(obj.ItemId, 1073741824);
						bindings.Update_Name(obj.Name, 1073741824);
					}
				}
				else if (!(propName == "ItemId"))
				{
					if (propName == "Name" && obj != null)
					{
						bindings.Update_Name(obj.Name, 1073741824);
					}
				}
				else if (obj != null)
				{
					bindings.Update_ItemId(obj.ItemId, 1073741824);
				}
			}

			public void UpdateChildListeners_(NamedListItem obj)
			{
				ProjectDetailsPage_obj83_Bindings bindings = TryGetBindingObject();
				if (bindings != null)
				{
					if (bindings.dataRoot != null)
					{
						((INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
					}
					if (obj != null)
					{
						bindings.dataRoot = obj;
						((INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
					}
				}
			}
		}

		private NamedListItem dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj83;

		private TextBlock obj84;

		private TextBlock obj85;

		private ProjectDetailsPage_obj83_BindingsTracking bindingsTracking;

		public ProjectDetailsPage_obj83_Bindings()
		{
			bindingsTracking = new ProjectDetailsPage_obj83_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 83:
				obj83 = new WeakReference(target.As<Grid>());
				break;
			case 84:
				obj84 = target.As<TextBlock>();
				break;
			case 85:
				obj85 = target.As<TextBlock>();
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					Grid rootElement = obj83.Target as Grid;
					if (rootElement != null)
					{
						rootElement.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<NamedListItem>(), 1 << phase);
		}

		public void Recycle()
		{
			bindingsTracking.ReleaseAllListeners();
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
			bindingsTracking.ReleaseAllListeners();
			initialized = false;
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			bindingsTracking.ReleaseAllListeners();
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<NamedListItem>();
				return true;
			}
			return false;
		}

		private void Update_(NamedListItem obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_(obj);
			if (obj != null && (phase & -1073741823) != 0)
			{
				Update_ItemId(obj.ItemId, phase);
				Update_Name(obj.Name, phase);
			}
		}

		private void Update_ItemId(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj84, obj, null);
			}
		}

		private void Update_Name(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(obj85, obj, null);
			}
		}
	}

	private readonly ObservableCollection<NamedListItem> groups = new ObservableCollection<NamedListItem>();

	private readonly ObservableCollection<NamedListItem> causes = new ObservableCollection<NamedListItem>();

	private readonly ObservableCollection<GroupSelectionOption> q1VinculoGroupOptions = new ObservableCollection<GroupSelectionOption>();

	private readonly ObservableCollection<AhpTableRow> ahpRows = new ObservableCollection<AhpTableRow>();

	private readonly ObservableCollection<Q1TableRow> q1Rows = new ObservableCollection<Q1TableRow>();

	private readonly List<Q1ParsedRow> q1ParsedRows = new List<Q1ParsedRow>();

	private readonly ObservableCollection<TopsisTableRow> q2Rows = new ObservableCollection<TopsisTableRow>();

	private readonly ObservableCollection<TopsisTableRow> reportTopTopsisRows = new ObservableCollection<TopsisTableRow>();

	private readonly List<TopsisAlternativeData> q2Alternatives = new List<TopsisAlternativeData>();

	private readonly Dictionary<string, double> ahpWeightsByGroupId = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

	private ProjectItem? currentProject;

	private bool isUpdatingValidationMedian;

	private bool isUpdatingValidationProportion;

	private bool isUpdatingAhpConsistency;

	private bool isValidationUiReady;

	private bool isProjectPivotHandlerAttached;

	private double? ahpCrPercent;

	private int ahpResponseCount;

	private int q1ResponseCount;

	private int q2ResponseCount;

	private const string CausesCsvFileName = "causas.csv";

	private const string ValidationCsvFileName = "q1.csv";

	private const string FinalAnswersCsvFileName = "q2.csv";

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Grid PageRootGrid;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsTopTopsisStatusTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView ReportsTopTopsisListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsTopCauseValueTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsCrGlobalValueTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsApprovedCausesValueTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ReportsRespondentsValueTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock TopsisStatusTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView Q2TableListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock TeamAhpTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView AhpTableListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock AhpGlobalCrTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Border AhpCrStatusBadgeBorder;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock AhpCrStatusBadgeTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private NumberBox AhpConsistencyNumberBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock TeamAssignmentsTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView Q1TableListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private CheckBox Scale5CheckBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private CheckBox Scale4CheckBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private CheckBox Scale3CheckBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private CheckBox Scale2CheckBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private CheckBox Scale1CheckBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private NumberBox ValidationProportionNumberBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private NumberBox ValidationMedianNumberBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock CausesStatusTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView CausesListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ItemsControl Q1VinculoGroupsItemsControl;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Button DeleteCauseButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Button RenameCauseButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock GroupsStatusTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private ListView GroupsListView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Button RenameGroupButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private Button DeleteGroupButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ProjectNameTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ProjectFolderTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ProjectCreatedTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock ProjectModifiedTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private TextBlock PageTitleTextBlock;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private bool _contentLoaded;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	private IProjectDetailsPage_Bindings Bindings;

	public ProjectDetailsPage()
	{
		InitializeComponent();
		ConfigureValidationNumberBoxes();
		base.Loaded += delegate
		{
			ApplyProjectTabHeadersAfterLayout();
			ApplyRemovedQ1RespondentGroupsUi();
		};
		base.LayoutUpdated += delegate
		{
			ApplyRemovedQ1RespondentGroupsUi();
		};
		GroupsListView.ItemsSource = groups;
		CausesListView.ItemsSource = causes;
		if ((object)Q1VinculoGroupsItemsControl != null)
		{
			Q1VinculoGroupsItemsControl.ItemsSource = q1VinculoGroupOptions;
		}
		EnsureAhpUiBindings();
		Q1TableListView.ItemsSource = q1Rows;
		Q2TableListView.ItemsSource = q2Rows;
		ReportsTopTopsisListView.ItemsSource = reportTopTopsisRows;
		Scale1CheckBox.IsChecked = false;
		Scale2CheckBox.IsChecked = false;
		Scale3CheckBox.IsChecked = true;
		Scale4CheckBox.IsChecked = true;
		Scale5CheckBox.IsChecked = true;
		ValidationMedianNumberBox.Text = ValidationMedianNumberBox.Value.ToString("F0", CultureInfo.CurrentCulture);
		ValidationProportionNumberBox.Text = ValidationProportionNumberBox.Value.ToString("F0", CultureInfo.CurrentCulture);
		if ((object)AhpConsistencyNumberBox != null)
		{
			AhpConsistencyNumberBox.Text = AhpConsistencyNumberBox.Value.ToString("F1", CultureInfo.CurrentCulture);
		}
		isValidationUiReady = true;
		UpdateGroupButtons();
		UpdateCauseButtons();
		UpdateGroupsStatus();
		UpdateCausesStatus();
		ApplyRemovedQ1RespondentGroupsUi();
	}

	private void ApplyRemovedQ1RespondentGroupsUi()
	{
		if ((object)Q1VinculoGroupsItemsControl != null)
		{
			Q1VinculoGroupsItemsControl.Visibility = Visibility.Collapsed;
		}
		HideTextBlockByExactText(PageRootGrid, "Grupos respondentes");
	}

	private async void ApplyProjectTabHeadersAfterLayout()
	{
		await Task.Delay(100);
		ApplyProjectTabHeaders(PageRootGrid);
		AttachProjectPivotHandler(PageRootGrid);
	}

	private void AttachProjectPivotHandler(DependencyObject root)
	{
		if (isProjectPivotHandlerAttached || root == null)
		{
			return;
		}
		if (root is Pivot pivot)
		{
			isProjectPivotHandlerAttached = true;
			pivot.SelectionChanged += delegate
			{
				ApplyValidationLabelsAfterTabChange();
			};
			return;
		}
		int childCount;
		try
		{
			childCount = VisualTreeHelper.GetChildrenCount(root);
		}
		catch
		{
			return;
		}
		for (int i = 0; i < childCount; i++)
		{
			AttachProjectPivotHandler(VisualTreeHelper.GetChild(root, i));
			if (isProjectPivotHandlerAttached)
			{
				return;
			}
		}
	}

	private async void ApplyValidationLabelsAfterTabChange()
	{
		await Task.Delay(150);
		ApplyUserFriendlyLabels();
	}

	private static void ApplyProjectTabHeaders(DependencyObject root)
	{
		if (root == null)
		{
			return;
		}
		if (root is PivotItem pivotItem && pivotItem.Header is string header)
		{
			pivotItem.Header = header.Trim() switch
			{
				"Dados" => "Projeto",
				"Grupos" => "Participantes",
				"Causas" => "Causas possíveis",
				"Validação" => "Aprovação",
				"Validacao" => "Aprovação",
				"AHP" => "Peso dos grupos",
				"TOPSIS" => "Ranking final",
				"Relatórios" => "Relatório",
				"Relatorios" => "Relatório",
				_ => header
			};
		}
		int childCount;
		try
		{
			childCount = VisualTreeHelper.GetChildrenCount(root);
		}
		catch
		{
			return;
		}
		for (int i = 0; i < childCount; i++)
		{
			ApplyProjectTabHeaders(VisualTreeHelper.GetChild(root, i));
		}
	}

	private static void HideTextBlockByExactText(DependencyObject root, string text)
	{
		if (root == null)
		{
			return;
		}
		if (root is TextBlock textBlock && string.Equals(textBlock.Text?.Trim(), text, StringComparison.OrdinalIgnoreCase))
		{
			textBlock.Visibility = Visibility.Collapsed;
		}
		int childCount = VisualTreeHelper.GetChildrenCount(root);
		for (int i = 0; i < childCount; i++)
		{
			HideTextBlockByExactText(VisualTreeHelper.GetChild(root, i), text);
		}
	}

	private static Button SetButtonText(object target, string text)
	{
		Button button = target.As<Button>();
		button.Content = text;
		AutomationProperties.SetName(button, text);
		return button;
	}

	private void ConfigureValidationNumberBoxes()
	{
		ConfigureIntegerNumberBox(ValidationMedianNumberBox, 1.0, 5.0, 3.0);
		ConfigureIntegerNumberBox(ValidationProportionNumberBox, 0.0, 100.0);
	}

	private static void ConfigureIntegerNumberBox(NumberBox numberBox, double minimum, double maximum, double? defaultValue = null)
	{
		if ((object)numberBox == null)
		{
			return;
		}
		numberBox.Minimum = minimum;
		numberBox.Maximum = maximum;
		numberBox.SmallChange = 1.0;
		if (defaultValue.HasValue)
		{
			numberBox.Value = defaultValue.Value;
			numberBox.Text = defaultValue.Value.ToString("F0", CultureInfo.CurrentCulture);
			return;
		}
		if (!double.IsNaN(numberBox.Value))
		{
			double roundedValue = RoundValidationInteger(numberBox.Value, minimum, maximum);
			numberBox.Value = roundedValue;
			numberBox.Text = roundedValue.ToString("F0", CultureInfo.CurrentCulture);
		}
	}

	private static double RoundValidationInteger(double value, double minimum, double maximum)
	{
		return Math.Clamp(Math.Round(value, 0, MidpointRounding.AwayFromZero), minimum, maximum);
	}

	private void ApplyUserFriendlyLabels()
	{
		if ((object)ValidationMedianNumberBox != null)
		{
			ValidationMedianNumberBox.Header = null;
		}
		ReplaceValidationLabelText(PageRootGrid);
		ReplaceValidationLabelText(Q1TableListView);
	}

	private async void RefreshUserFriendlyLabelsAfterLayout()
	{
		ApplyUserFriendlyLabels();
		base.DispatcherQueue.TryEnqueue(ApplyUserFriendlyLabels);
		await Task.Delay(50);
		ApplyUserFriendlyLabels();
		await Task.Delay(250);
		ApplyUserFriendlyLabels();
	}

	private static void ReplaceValidationLabelText(DependencyObject root)
	{
		if (root == null)
		{
			return;
		}
		if (root is TextBlock textBlock)
		{
			textBlock.Text = ReplaceValidationLabelText(textBlock.Text);
		}
		int childCount;
		try
		{
			childCount = VisualTreeHelper.GetChildrenCount(root);
		}
		catch
		{
			return;
		}
		for (int i = 0; i < childCount; i++)
		{
			ReplaceValidationLabelText(VisualTreeHelper.GetChild(root, i));
		}
	}

	private static string ReplaceValidationLabelText(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return text.Trim() switch
		{
			"Média" => "Mediana",
			"Media" => "Mediana",
			"Média mínima" => "Mediana mínima",
			"Media minima" => "Mediana mínima",
			"Proporção" => "Concordância",
			"Proporcao" => "Concordância",
			"Proporção (%)" => "Concordância mínima (%)",
			"Proporcao (%)" => "Concordância mínima (%)",
			"Escala" => "Notas positivas",
			"Status" => "Resultado",
			"Id" => "Código",
			"Causa" => "Causa possível",
			_ => text
		};
	}

	protected override void OnNavigatedTo(NavigationEventArgs e)
	{
		base.OnNavigatedTo(e);
		EnsureAhpUiBindings();
		if (!(e.Parameter is ProjectItem project))
		{
			currentProject = null;
			groups.Clear();
			causes.Clear();
			ahpRows.Clear();
			q1Rows.Clear();
			q1ParsedRows.Clear();
			q2Rows.Clear();
			reportTopTopsisRows.Clear();
			q2Alternatives.Clear();
			q1ResponseCount = 0;
			q2ResponseCount = 0;
			q1VinculoGroupOptions.Clear();
			ClearAhpComputedData();
			UpdateGroupButtons();
			UpdateCauseButtons();
			UpdateGroupsStatus();
			UpdateCausesStatus();
			TopsisStatusTextBlock.Text = string.Empty;
			RebuildAhpTableRows();
			RefreshReportsSection();
		}
		else
		{
			currentProject = project;
			string projectDisplayName = BuildProjectDisplayName(project);
			PageTitleTextBlock.Text = "Projeto: " + projectDisplayName;
			ProjectNameTextBlock.Text = projectDisplayName;
			ProjectFolderTextBlock.Text = project.FolderPath;
			ProjectCreatedTextBlock.Text = project.CreatedAtText;
			ProjectModifiedTextBlock.Text = project.ModifiedAtText;
			LoadGroups();
			LoadCauses();
			LoadQ1TableForCurrentProject();
			LoadQ2TableForCurrentProject();
			RefreshReportsSection();
		}
	}

	private static string BuildProjectDisplayName(ProjectItem project)
	{
		string institution = (project.Name ?? string.Empty).Trim();
		string course = (project.Course ?? string.Empty).Trim();
		if (!string.IsNullOrWhiteSpace(institution) && !string.IsNullOrWhiteSpace(course))
		{
			return course + " - " + institution;
		}
		if (string.IsNullOrWhiteSpace(institution))
		{
			return course;
		}
		return institution;
	}

	private void RefreshReportsSection()
	{
		if ((object)ReportsTextBlock == null)
		{
			return;
		}
		int approvedCauses = q1Rows.Count((Q1TableRow q1TableRow) => string.Equals(q1TableRow.Status, "Aprovada", StringComparison.OrdinalIgnoreCase));
		int totalCauses = q1Rows.Count;
		ReportsRespondentsValueTextBlock.Text = $"Aprovação: {q1ResponseCount} | Final: {q2ResponseCount}";
		ReportsApprovedCausesValueTextBlock.Text = ((totalCauses > 0) ? $"{approvedCauses}/{totalCauses}" : "-");
		if (ahpCrPercent.HasValue)
		{
			double limit = (((object)AhpConsistencyNumberBox != null && !double.IsNaN(AhpConsistencyNumberBox.Value)) ? AhpConsistencyNumberBox.Value : 0.0);
			string consistencyStatus = ((ahpCrPercent.Value <= limit) ? "Consistente" : "Inconsistente");
			ReportsCrGlobalValueTextBlock.Text = ahpCrPercent.Value.ToString("F1", CultureInfo.CurrentCulture) + "% (" + consistencyStatus + ")";
		}
		else
		{
			ReportsCrGlobalValueTextBlock.Text = "-";
		}
		TopsisTableRow topCause = q2Rows.OrderBy((TopsisTableRow topsisTableRow) => TryParseRank(topsisTableRow.Rank)).FirstOrDefault();
		ReportsTopCauseValueTextBlock.Text = ((topCause == null) ? "-" : (topCause.Id + " - " + topCause.Cause));
		reportTopTopsisRows.Clear();
		foreach (TopsisTableRow row in q2Rows.OrderBy((TopsisTableRow item) => TryParseRank(item.Rank)).Take(10))
		{
			reportTopTopsisRows.Add(new TopsisTableRow(row.Id, row.Cause, row.Rank));
		}
		ReportsTopTopsisStatusTextBlock.Text = ((reportTopTopsisRows.Count > 0) ? $"Top {reportTopTopsisRows.Count} causas no ranking final." : "Importe as respostas finais para exibir o ranking.");
		string q1Import = BuildFileImportLabel(ValidationCsvFileName);
		string q2Import = BuildFileImportLabel(FinalAnswersCsvFileName);
		string causesImport = BuildFileImportLabel(CausesCsvFileName);
		ReportsTextBlock.Text = $"Últimas importações: aprovação ({q1Import}) | ranking final ({q2Import}) | causas ({causesImport}).";
	}

	private static int TryParseRank(string rank)
	{
		if (!int.TryParse(rank, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
		{
			return int.MaxValue;
		}
		return parsed;
	}

	private string BuildFileImportLabel(string fileName)
	{
		string displayName = BuildUserFileName(fileName);
		if (currentProject == null)
		{
			return displayName + ": -";
		}
		string filePath = Path.Combine(currentProject.FolderPath, fileName);
		if (!File.Exists(filePath))
		{
			return displayName + ": não importado";
		}
		try
		{
			DateTime modifiedAt = File.GetLastWriteTime(filePath);
			return $"{displayName}: {modifiedAt:dd/MM/yyyy HH:mm}";
		}
		catch
		{
			return displayName + ": erro ao ler data";
		}
	}

	private static string BuildUserFileName(string fileName)
	{
		return fileName switch
		{
			ValidationCsvFileName => "respostas de aprovação",
			FinalAnswersCsvFileName => "respostas do ranking final",
			CausesCsvFileName => "lista de causas",
			_ => fileName
		};
	}

	private string BuildReportsSummaryText()
	{
		int approvedCauses = q1Rows.Count((Q1TableRow q1TableRow) => string.Equals(q1TableRow.Status, "Aprovada", StringComparison.OrdinalIgnoreCase));
		int totalCauses = q1Rows.Count;
		double consistencyLimit = (((object)AhpConsistencyNumberBox != null && !double.IsNaN(AhpConsistencyNumberBox.Value)) ? AhpConsistencyNumberBox.Value : 0.0);
		string projectName = ((currentProject == null) ? "-" : BuildProjectDisplayName(currentProject));
		string ahpCrSummary = (ahpCrPercent.HasValue ? (ahpCrPercent.Value.ToString("F1", CultureInfo.CurrentCulture) + "% (limite " + consistencyLimit.ToString("F1", CultureInfo.CurrentCulture) + "%)") : "-");
		StringBuilder builder = new StringBuilder();
		StringBuilder stringBuilder = builder;
		StringBuilder stringBuilder2 = stringBuilder;
		StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(9, 1, stringBuilder);
		handler.AppendLiteral("Projeto: ");
		handler.AppendFormatted(projectName);
		stringBuilder2.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder3 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(17, 1, stringBuilder);
		handler.AppendLiteral("Respondentes da aprovação: ");
		handler.AppendFormatted(q1ResponseCount);
		stringBuilder3.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder4 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(17, 1, stringBuilder);
		handler.AppendLiteral("Respondentes finais: ");
		handler.AppendFormatted(q2ResponseCount);
		stringBuilder4.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder5 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(19, 2, stringBuilder);
		handler.AppendLiteral("Causas aprovadas: ");
		handler.AppendFormatted(approvedCauses);
		handler.AppendLiteral("/");
		handler.AppendFormatted(totalCauses);
		stringBuilder5.AppendLine(ref handler);
		builder.AppendLine("Critérios de aprovação: " + BuildApprovalCriteriaSummary());
		if (groups.Count > 0)
		{
			builder.AppendLine("Peso dos grupos:");
			foreach (NamedListItem group in groups)
			{
				string weightText = ahpWeightsByGroupId.TryGetValue(group.ItemId, out var weight) ? ((weight * 100.0).ToString("F1", CultureInfo.CurrentCulture) + "%") : "-";
				builder.AppendLine(group.ItemId + " - " + group.Name + ": " + weightText);
			}
		}
		stringBuilder = builder;
		StringBuilder stringBuilder6 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(15, 1, stringBuilder);
		handler.AppendLiteral("Consistência das comparações: ");
		handler.AppendFormatted(ahpCrSummary);
		stringBuilder6.AppendLine(ref handler);
		builder.AppendLine("Top 5 do ranking final:");
		foreach (TopsisTableRow row in q2Rows.OrderBy((TopsisTableRow item) => TryParseRank(item.Rank)).Take(5))
		{
			stringBuilder = builder;
			StringBuilder stringBuilder7 = stringBuilder;
			handler = new StringBuilder.AppendInterpolatedStringHandler(5, 3, stringBuilder);
			handler.AppendFormatted(row.Rank);
			handler.AppendLiteral(". ");
			handler.AppendFormatted(row.Id);
			handler.AppendLiteral(" - ");
			handler.AppendFormatted(row.Cause);
			stringBuilder7.AppendLine(ref handler);
		}
		return builder.ToString().TrimEnd();
	}

	private string BuildReportsCsvContent()
	{
		int approvedCauses = q1Rows.Count((Q1TableRow q1TableRow) => string.Equals(q1TableRow.Status, "Aprovada", StringComparison.OrdinalIgnoreCase));
		int totalCauses = q1Rows.Count;
		double consistencyLimit = (((object)AhpConsistencyNumberBox != null && !double.IsNaN(AhpConsistencyNumberBox.Value)) ? AhpConsistencyNumberBox.Value : 0.0);
		string ahpCrValue = (ahpCrPercent.HasValue ? ahpCrPercent.Value.ToString("F1", CultureInfo.CurrentCulture) : "-");
		StringBuilder builder = new StringBuilder();
		builder.AppendLine("Secao;Campo;Valor");
		StringBuilder stringBuilder = builder;
		StringBuilder stringBuilder2 = stringBuilder;
		StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(23, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Respondentes_Aprovacao;");
		handler.AppendFormatted(q1ResponseCount.ToString(CultureInfo.InvariantCulture));
		stringBuilder2.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder3 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(23, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Respondentes_Final;");
		handler.AppendFormatted(q2ResponseCount.ToString(CultureInfo.InvariantCulture));
		stringBuilder3.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder4 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(24, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Causas_Aprovadas;");
		handler.AppendFormatted(approvedCauses.ToString(CultureInfo.InvariantCulture));
		stringBuilder4.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder5 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(20, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Causas_Total;");
		handler.AppendFormatted(totalCauses.ToString(CultureInfo.InvariantCulture));
		stringBuilder5.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder6 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(21, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Consistencia_Comparacoes;");
		handler.AppendFormatted(EscapeCsvValue(ahpCrValue));
		stringBuilder6.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder7 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(21, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Limite_Inconsistencia;");
		handler.AppendFormatted(EscapeCsvValue(consistencyLimit.ToString("F1", CultureInfo.CurrentCulture)));
		stringBuilder7.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder8 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(21, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Importacao_Aprovacao;");
		handler.AppendFormatted(EscapeCsvValue(BuildFileImportLabel(ValidationCsvFileName)));
		stringBuilder8.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder9 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(21, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Importacao_Final;");
		handler.AppendFormatted(EscapeCsvValue(BuildFileImportLabel(FinalAnswersCsvFileName)));
		stringBuilder9.AppendLine(ref handler);
		stringBuilder = builder;
		StringBuilder stringBuilder10 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(25, 1, stringBuilder);
		handler.AppendLiteral("Resumo;Importacao_causas;");
		handler.AppendFormatted(EscapeCsvValue(BuildFileImportLabel(CausesCsvFileName)));
		stringBuilder10.AppendLine(ref handler);
		builder.AppendLine("Criterios;Mediana_Minima;" + EscapeCsvValue(BuildValidationMedianThreshold().ToString("F0", CultureInfo.CurrentCulture)));
		builder.AppendLine("Criterios;Concordancia_Minima_Percentual;" + EscapeCsvValue(BuildValidationProportionThreshold().ToString("F0", CultureInfo.CurrentCulture)));
		builder.AppendLine("Criterios;Notas_Consideradas_Positivas;" + EscapeCsvValue(BuildSelectedScaleValuesText()));
		foreach (NamedListItem group in groups)
		{
			string weightText = ahpWeightsByGroupId.TryGetValue(group.ItemId, out var weight) ? ((weight * 100.0).ToString("F1", CultureInfo.CurrentCulture) + "%") : "-";
			builder.AppendLine("Peso_Grupos;" + EscapeCsvValue(group.ItemId + " - " + group.Name) + ";" + EscapeCsvValue(weightText));
		}
		foreach (TopsisTableRow row in q2Rows.OrderBy((TopsisTableRow item) => TryParseRank(item.Rank)).Take(10))
		{
			stringBuilder = builder;
			StringBuilder stringBuilder11 = stringBuilder;
			handler = new StringBuilder.AppendInterpolatedStringHandler(8, 2, stringBuilder);
			handler.AppendLiteral("Ranking_Final;");
			handler.AppendFormatted(EscapeCsvValue(row.Rank));
			handler.AppendLiteral(";");
			handler.AppendFormatted(EscapeCsvValue(row.Id + " - " + row.Cause));
			stringBuilder11.AppendLine(ref handler);
		}
		return builder.ToString();
	}

	private string BuildApprovalCriteriaSummary()
	{
		return "mediana mínima " + BuildValidationMedianThreshold().ToString("F0", CultureInfo.CurrentCulture) + "; concordância mínima " + BuildValidationProportionThreshold().ToString("F0", CultureInfo.CurrentCulture) + "%; notas positivas " + BuildSelectedScaleValuesText();
	}

	private double BuildValidationMedianThreshold()
	{
		return ((object)ValidationMedianNumberBox != null && !double.IsNaN(ValidationMedianNumberBox.Value)) ? RoundValidationInteger(ValidationMedianNumberBox.Value, 1.0, 5.0) : 3.0;
	}

	private double BuildValidationProportionThreshold()
	{
		return ((object)ValidationProportionNumberBox != null && !double.IsNaN(ValidationProportionNumberBox.Value)) ? RoundValidationInteger(ValidationProportionNumberBox.Value, 0.0, 100.0) : 70.0;
	}

	private string BuildSelectedScaleValuesText()
	{
		HashSet<int> values = ((object)Scale1CheckBox == null) ? new HashSet<int> { 3, 4, 5 } : GetSelectedScaleValues();
		if (values.Count == 0)
		{
			return "nenhuma";
		}
		return string.Join(", ", values.OrderBy((int value) => value));
	}

	private static string EscapeCsvValue(string value)
	{
		string normalized = value ?? string.Empty;
		if (!normalized.Contains(';') && !normalized.Contains('"') && !normalized.Contains('\r') && !normalized.Contains('\n'))
		{
			return normalized;
		}
		return "\"" + normalized.Replace("\"", "\"\"", StringComparison.Ordinal) + "\"";
	}

	private void EnsureAhpUiBindings()
	{
		if ((object)AhpTableListView != null && AhpTableListView.ItemsSource != ahpRows)
		{
			AhpTableListView.ItemsSource = ahpRows;
		}
	}

	private void OnGroupsSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		UpdateGroupButtons();
	}

	private async void OnAddGroupClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		TextBox groupNameTextBox = new TextBox
		{
			PlaceholderText = "Nome do grupo participante",
			MinWidth = 320.0
		};
		if (await new ContentDialog
		{
			Title = "Adicionar grupo participante",
			Content = groupNameTextBox,
			PrimaryButtonText = "Adicionar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Primary,
			XamlRoot = base.XamlRoot
		}.ShowAsync() != ContentDialogResult.Primary)
		{
			return;
		}
		string groupName = groupNameTextBox.Text.Trim();
		if (!string.IsNullOrWhiteSpace(groupName))
		{
			if (groups.Any((NamedListItem existingItem) => string.Equals(existingItem.Name, groupName, StringComparison.OrdinalIgnoreCase)))
			{
				await ShowMessageAsync("Grupo existente", "Já existe um grupo participante com esse nome.");
				return;
			}
			NamedListItem newItem = new NamedListItem(groupName, "G");
			groups.Add(newItem);
			RenumberItems(groups);
			RebuildQ1VinculoGroupOptions();
			GroupsListView.SelectedItem = newItem;
			GroupsListView.UpdateLayout();
			SaveGroups();
			UpdateGroupButtons();
			UpdateGroupsStatus("Grupo participante adicionado.");
			RebuildAhpTableRows("Grupo participante adicionado.");
			LoadQ2TableForCurrentProject();
		}
	}

	private async void OnRenameGroupClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		object selectedItem = GroupsListView.SelectedItem;
		NamedListItem selectedGroup = selectedItem as NamedListItem;
		if (selectedGroup == null)
		{
			return;
		}
		TextBox groupNameTextBox = new TextBox
		{
			Text = selectedGroup.Name,
			PlaceholderText = "Novo nome do grupo participante",
			MinWidth = 320.0
		};
		if (await new ContentDialog
		{
			Title = "Editar grupo participante",
			Content = groupNameTextBox,
			PrimaryButtonText = "Salvar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Primary,
			XamlRoot = base.XamlRoot
		}.ShowAsync() != ContentDialogResult.Primary)
		{
			return;
		}
		string newGroupName = groupNameTextBox.Text.Trim();
		if (!string.IsNullOrWhiteSpace(newGroupName))
		{
			if (groups.Any((NamedListItem existingItem) => existingItem != selectedGroup && string.Equals(existingItem.Name, newGroupName, StringComparison.OrdinalIgnoreCase)))
			{
				await ShowMessageAsync("Grupo existente", "Já existe um grupo participante com esse nome.");
				return;
			}
			selectedGroup.Name = newGroupName;
			RebuildQ1VinculoGroupOptions();
			GroupsListView.UpdateLayout();
			SaveGroups();
			UpdateGroupButtons();
			UpdateGroupsStatus("Grupo participante renomeado.");
			RebuildAhpTableRows("Grupo participante renomeado.");
			LoadQ2TableForCurrentProject();
		}
	}

	private async void OnDeleteGroupClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null)
		{
			object selectedItem = GroupsListView.SelectedItem;
			if (selectedItem is NamedListItem selectedGroup && await new ContentDialog
			{
				Title = "Confirmar exclusão",
				Content = "Deseja realmente excluir o grupo participante \"" + selectedGroup.Name + "\"?",
				PrimaryButtonText = "Excluir",
				CloseButtonText = "Cancelar",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = base.XamlRoot
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				groups.Remove(selectedGroup);
				RenumberItems(groups);
				RebuildQ1VinculoGroupOptions();
				GroupsListView.UpdateLayout();
				SaveGroups();
				UpdateGroupButtons();
				UpdateGroupsStatus("Grupo participante excluído.");
				RebuildAhpTableRows("Grupo participante excluído.");
				LoadQ2TableForCurrentProject();
			}
		}
	}

	private async void OnClearGroupsClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null && groups.Count != 0 && await new ContentDialog
		{
			Title = "Confirmar limpeza",
			Content = $"Deseja realmente limpar os grupos participantes ({groups.Count})?",
			PrimaryButtonText = "Limpar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Close,
			XamlRoot = base.XamlRoot
		}.ShowAsync() == ContentDialogResult.Primary)
		{
			groups.Clear();
			q1VinculoGroupOptions.Clear();
			GroupsListView.UpdateLayout();
			ClearAhpComputedData();
			SaveGroups();
			UpdateGroupButtons();
			UpdateGroupsStatus("Grupos participantes limpos.");
			RebuildAhpTableRows("Grupos participantes limpos.");
			LoadQ2TableForCurrentProject();
		}
	}

	private void LoadGroups()
	{
		groups.Clear();
		if (currentProject == null)
		{
			q1VinculoGroupOptions.Clear();
			UpdateGroupButtons();
			UpdateGroupsStatus();
			RebuildAhpTableRows();
			LoadQ2TableForCurrentProject();
			RefreshReportsSection();
			return;
		}
		foreach (string groupName in ProjectGroupsRepository.LoadGroups(currentProject.FolderPath))
		{
			groups.Add(new NamedListItem(groupName, "G"));
		}
		RenumberItems(groups);
		RebuildQ1VinculoGroupOptions();
		GroupsListView.UpdateLayout();
		UpdateGroupButtons();
		UpdateGroupsStatus();
		RebuildAhpTableRows();
		LoadQ2TableForCurrentProject();
		RefreshReportsSection();
	}

	private void RebuildQ1VinculoGroupOptions()
	{
		q1VinculoGroupOptions.Clear();
		ApplyRemovedQ1RespondentGroupsUi();
	}

	private void SaveGroups()
	{
		if (currentProject == null)
		{
			return;
		}
		try
		{
			ProjectGroupsRepository.SaveGroups(currentProject.FolderPath, groups.Select((NamedListItem item) => item.Name));
			MarkProjectAsModified();
		}
		catch
		{
			UpdateGroupsStatus("Erro ao salvar grupos.");
		}
	}

	private void MarkProjectAsModified()
	{
		if (currentProject == null)
		{
			return;
		}
		try
		{
			currentProject.ModifiedAt = DateTimeOffset.Now;
			ProjectModifiedTextBlock.Text = currentProject.ModifiedAtText;
			PersistCurrentProjectMetadata();
		}
		catch
		{
		}
	}

	private void PersistCurrentProjectMetadata()
	{
		if (currentProject == null)
		{
			return;
		}
		List<ProjectItem> projects = (from project in ProjectRepository.LoadProjects()
			select new ProjectItem(project.Name, project.Course, project.CreatedAt, project.ModifiedAt, project.FolderPath)).ToList();
		int index = projects.FindIndex((ProjectItem project) => !string.IsNullOrWhiteSpace(project.FolderPath) && string.Equals(project.FolderPath, currentProject.FolderPath, StringComparison.OrdinalIgnoreCase));
		if (index < 0)
		{
			index = projects.FindIndex((ProjectItem project) => string.Equals(project.Name, currentProject.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(project.Course, currentProject.Course, StringComparison.OrdinalIgnoreCase));
		}
		if (index >= 0)
		{
			projects[index].Name = currentProject.Name;
			projects[index].Course = currentProject.Course;
			projects[index].FolderPath = currentProject.FolderPath;
			projects[index].ModifiedAt = currentProject.ModifiedAt;
		}
		else
		{
			projects.Add(new ProjectItem(currentProject.Name, currentProject.Course, currentProject.CreatedAt, currentProject.ModifiedAt, currentProject.FolderPath));
		}
		ProjectRepository.SaveProjects(projects);
	}

	private void UpdateGroupButtons()
	{
		bool hasSelection = GroupsListView.SelectedItem is NamedListItem;
		RenameGroupButton.IsEnabled = hasSelection;
		DeleteGroupButton.IsEnabled = hasSelection;
	}

	private void OnCausesSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		UpdateCauseButtons();
	}

	private async void OnAddCauseClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		TextBox causeNameTextBox = new TextBox
		{
			PlaceholderText = "Nome da causa possível",
			MinWidth = 320.0
		};
		if (await new ContentDialog
		{
			Title = "Adicionar causa possível",
			Content = causeNameTextBox,
			PrimaryButtonText = "Adicionar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Primary,
			XamlRoot = base.XamlRoot
		}.ShowAsync() != ContentDialogResult.Primary)
		{
			return;
		}
		string causeName = causeNameTextBox.Text.Trim();
		if (!string.IsNullOrWhiteSpace(causeName))
		{
			if (causes.Any((NamedListItem existingItem) => string.Equals(existingItem.Name, causeName, StringComparison.OrdinalIgnoreCase)))
			{
				await ShowMessageAsync("Causa existente", "Já existe uma causa possível com esse nome.");
				return;
			}
			NamedListItem newItem = new NamedListItem(causeName, "C");
			causes.Add(newItem);
			RenumberItems(causes);
			CausesListView.SelectedItem = newItem;
			SaveCauses();
			UpdateCauseButtons();
			UpdateCausesStatus("Causa possível adicionada.");
		}
	}

	private async void OnRenameCauseClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		object selectedItem = CausesListView.SelectedItem;
		NamedListItem selectedCause = selectedItem as NamedListItem;
		if (selectedCause == null)
		{
			return;
		}
		TextBox causeNameTextBox = new TextBox
		{
			Text = selectedCause.Name,
			PlaceholderText = "Novo nome da causa possível",
			MinWidth = 320.0
		};
		if (await new ContentDialog
		{
			Title = "Editar causa possível",
			Content = causeNameTextBox,
			PrimaryButtonText = "Salvar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Primary,
			XamlRoot = base.XamlRoot
		}.ShowAsync() != ContentDialogResult.Primary)
		{
			return;
		}
		string newCauseName = causeNameTextBox.Text.Trim();
		if (!string.IsNullOrWhiteSpace(newCauseName))
		{
			if (causes.Any((NamedListItem existingItem) => existingItem != selectedCause && string.Equals(existingItem.Name, newCauseName, StringComparison.OrdinalIgnoreCase)))
			{
				await ShowMessageAsync("Causa existente", "Já existe uma causa possível com esse nome.");
				return;
			}
			selectedCause.Name = newCauseName;
			SaveCauses();
			UpdateCauseButtons();
			UpdateCausesStatus("Causa possível renomeada.");
		}
	}

	private async void OnDeleteCauseClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null)
		{
			object selectedItem = CausesListView.SelectedItem;
			if (selectedItem is NamedListItem selectedCause && await new ContentDialog
			{
				Title = "Confirmar exclusão",
				Content = "Deseja realmente excluir a causa possível \"" + selectedCause.Name + "\"?",
				PrimaryButtonText = "Excluir",
				CloseButtonText = "Cancelar",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = base.XamlRoot
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				causes.Remove(selectedCause);
				RenumberItems(causes);
				SaveCauses();
				UpdateCauseButtons();
				UpdateCausesStatus("Causa possível excluída.");
			}
		}
	}

	private async void OnClearCausesClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null)
		{
			if (causes.Count == 0)
			{
				UpdateCausesStatus("Causas possíveis limpas.");
			}
			else if (await new ContentDialog
			{
				Title = "Confirmar limpeza",
				Content = $"Deseja realmente limpar as causas possíveis ({causes.Count})?",
				PrimaryButtonText = "Limpar",
				CloseButtonText = "Cancelar",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = base.XamlRoot
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				causes.Clear();
				SaveCauses();
				UpdateCauseButtons();
				UpdateCausesStatus("Causas possíveis limpas.");
			}
		}
	}

	private async void OnImportCausesCsvClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		string sourceCsvPath = await PickCsvFilePathAsync();
		if (string.IsNullOrWhiteSpace(sourceCsvPath))
		{
			return;
		}
		string destinationCsvPath = Path.Combine(currentProject.FolderPath, CausesCsvFileName);
		try
		{
			Directory.CreateDirectory(currentProject.FolderPath);
			File.Copy(sourceCsvPath, destinationCsvPath, overwrite: true);
		}
		catch (Exception ex)
		{
			UpdateCausesStatus("Erro ao importar a lista de causas.");
			await ShowMessageAsync("Erro ao importar", "Não foi possível importar a lista de causas.\n\n" + ex.Message);
			return;
		}
		CauseImportResult result = ProjectCausesRepository.ImportFromCsvFile(destinationCsvPath, causes.Select((NamedListItem item) => item.Name));
		if (!result.FileFound)
		{
			UpdateCausesStatus("Erro ao importar a lista de causas.");
			await ShowMessageAsync("Arquivo não encontrado", "Arquivo não encontrado em:\n" + result.CsvPath);
			return;
		}
		foreach (string causeName in result.AddedCauses)
		{
			causes.Add(new NamedListItem(causeName, "C"));
		}
		if (result.AddedCount > 0)
		{
			RenumberItems(causes);
			SaveCauses();
		}
		await ShowMessageAsync("Importação concluída", $"Arquivo importado para:\n{destinationCsvPath}\n\nAdicionadas: {result.AddedCount}\nDuplicadas ignoradas: {result.SkippedDuplicates}\nLinhas inválidas: {result.InvalidRows}");
		if (result.AddedCount == 0)
		{
			MarkProjectAsModified();
		}
		UpdateCauseButtons();
		UpdateCausesStatus("Importação concluída.");
	}

	private async void OnDownloadCausesTemplateClicked(object sender, RoutedEventArgs e)
	{
		string csvSavePath = await PickCsvSavePathAsync(BuildSuggestedFileName("modelo_causas_evasao"));
		if (!string.IsNullOrWhiteSpace(csvSavePath))
		{
			try
			{
				ProjectCausesRepository.ExportTemplateCsvToFile(csvSavePath, overwriteExisting: true);
				await ShowMessageAsync("Modelo salvo", "Modelo da lista de causas salvo em:\n" + csvSavePath);
				UpdateCausesStatus("Modelo da lista de causas exportado.");
			}
			catch (Exception ex)
			{
				await ShowMessageAsync("Erro ao baixar modelo", "Não foi possível salvar o modelo da lista de causas.\n\n" + ex.Message);
				UpdateCausesStatus("Erro ao exportar o modelo da lista de causas.");
			}
		}
	}

	private async Task<string?> ShowImportCausesDialogAsync()
	{
		if ((object)App.MainWindowInstance == null)
		{
			await ShowMessageAsync("Erro", "Janela principal não encontrada para abrir seletor de arquivo.");
			return null;
		}
		string selectedCsvPath = null;
		TextBlock selectedFileText = new TextBlock
		{
			Text = "Nenhum arquivo selecionado.",
			TextWrapping = TextWrapping.WrapWholeWords
		};
		TextBlock dropHintText = new TextBlock
		{
			Text = "Arraste e solte aqui um arquivo .csv",
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Center
		};
		Border dropArea = new Border
		{
			BorderThickness = new Thickness(1.0),
			BorderBrush = (Application.Current.Resources["CardStrokeColorDefaultBrush"] as Brush),
			CornerRadius = new CornerRadius(8.0),
			Padding = new Thickness(16.0),
			Height = 90.0,
			AllowDrop = true,
			Child = dropHintText
		};
		ContentDialog dialog = null;
		dropArea.DragOver += delegate(object _, DragEventArgs args)
		{
			args.AcceptedOperation = DataPackageOperation.Copy;
		};
		dropArea.Drop += async delegate(object _, DragEventArgs args)
		{
			if (args.DataView.Contains(StandardDataFormats.StorageItems))
			{
				StorageFile csvFile = (await args.DataView.GetStorageItemsAsync()).OfType<StorageFile>().FirstOrDefault((StorageFile file) => string.Equals(Path.GetExtension(file.Name), ".csv", StringComparison.OrdinalIgnoreCase));
				if ((object)csvFile == null)
				{
					selectedFileText.Text = "Nenhum arquivo CSV valido foi arrastado.";
				}
				else
				{
					selectedCsvPath = csvFile.Path;
					selectedFileText.Text = "Arquivo selecionado: " + selectedCsvPath;
					if ((object)dialog != null)
					{
						dialog.IsPrimaryButtonEnabled = true;
					}
				}
			}
		};
		Button browseButton = new Button
		{
			Content = "Procurar arquivo...",
			HorizontalAlignment = HorizontalAlignment.Left
		};
		browseButton.Click += async delegate
		{
			string csvPath = await PickCsvFilePathAsync();
			if (!string.IsNullOrWhiteSpace(csvPath))
			{
				selectedCsvPath = csvPath;
				selectedFileText.Text = "Arquivo selecionado: " + selectedCsvPath;
				if ((object)dialog != null)
				{
					dialog.IsPrimaryButtonEnabled = true;
				}
			}
		};
		TextBlock exportStatusText = new TextBlock
		{
			TextWrapping = TextWrapping.WrapWholeWords
		};
		Button exportTemplateButton = new Button
		{
			Content = "Baixar modelo de causas",
			HorizontalAlignment = HorizontalAlignment.Left
		};
		exportTemplateButton.Click += async delegate
		{
			string csvSavePath = await PickCsvSavePathAsync(BuildSuggestedFileName("modelo_causas_evasao"));
			if (string.IsNullOrWhiteSpace(csvSavePath))
			{
				exportStatusText.Text = "Download do modelo cancelado.";
				return;
			}
			try
			{
				ProjectCausesRepository.ExportTemplateCsvToFile(csvSavePath, overwriteExisting: true);
				exportStatusText.Text = "Modelo salvo em: " + csvSavePath;
			}
			catch (Exception ex)
			{
				exportStatusText.Text = "Erro ao salvar modelo: " + ex.Message;
			}
		};
		StackPanel contentPanel = new StackPanel
		{
			Spacing = 12.0
		};
		contentPanel.Children.Add(dropArea);
		contentPanel.Children.Add(browseButton);
		contentPanel.Children.Add(exportTemplateButton);
		contentPanel.Children.Add(selectedFileText);
		contentPanel.Children.Add(exportStatusText);
		dialog = new ContentDialog
		{
			Title = "Importar lista de causas",
			Content = contentPanel,
			PrimaryButtonText = "Importar",
			CloseButtonText = "Cancelar",
			DefaultButton = ContentDialogButton.Primary,
			IsPrimaryButtonEnabled = false,
			XamlRoot = base.XamlRoot
		};
		return (await dialog.ShowAsync() == ContentDialogResult.Primary) ? selectedCsvPath : null;
	}

	private async Task<string?> PickCsvFilePathAsync()
	{
		if ((object)App.MainWindowInstance == null)
		{
			return null;
		}
		FileOpenPicker obj = new FileOpenPicker
		{
			SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
			ViewMode = PickerViewMode.List,
			FileTypeFilter = { ".csv" }
		};
		InitializeWithWindow.Initialize(obj, WindowNative.GetWindowHandle(App.MainWindowInstance));
		return (await obj.PickSingleFileAsync())?.Path;
	}

	private async Task<string?> PickCsvSavePathAsync(string suggestedFileName = "lista_causas_evasao")
	{
		if ((object)App.MainWindowInstance == null)
		{
			return null;
		}
		FileSavePicker obj = new FileSavePicker
		{
			SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
			SuggestedFileName = suggestedFileName,
			FileTypeChoices = { 
			{
				"CSV",
				(IList<string>)new List<string> { ".csv" }
			} }
		};
		InitializeWithWindow.Initialize(obj, WindowNative.GetWindowHandle(App.MainWindowInstance));
		return (await obj.PickSaveFileAsync())?.Path;
	}

	private string BuildSuggestedFileName(string baseName)
	{
		if (currentProject == null)
		{
			return baseName;
		}
		string projectSlug = BuildFileNameSlug(BuildProjectDisplayName(currentProject));
		return string.IsNullOrWhiteSpace(projectSlug) ? baseName : (baseName + "_" + projectSlug);
	}

	private static string BuildFileNameSlug(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}
		StringBuilder builder = new StringBuilder(value.Length);
		bool previousWasSeparator = false;
		foreach (char c in value.Trim())
		{
			if (char.IsLetterOrDigit(c))
			{
				builder.Append(char.ToLowerInvariant(c));
				previousWasSeparator = false;
			}
			else if (!previousWasSeparator && builder.Length > 0)
			{
				builder.Append('_');
				previousWasSeparator = true;
			}
		}
		return builder.ToString().Trim('_');
	}

	private void LoadQ1TableForCurrentProject()
	{
		if (currentProject == null)
		{
			q1Rows.Clear();
			q1ParsedRows.Clear();
			q1ResponseCount = 0;
			TeamAssignmentsTextBlock.Text = string.Empty;
			ClearAhpComputedData();
			RebuildAhpTableRows();
		}
		else
		{
			string csvPath = Path.Combine(currentProject.FolderPath, ValidationCsvFileName);
			LoadQ1TableFromFile(csvPath);
		}
	}

	private void LoadQ1TableFromFile(string csvPath)
	{
		q1Rows.Clear();
		q1ParsedRows.Clear();
		q1ResponseCount = 0;
		if (!File.Exists(csvPath))
		{
			TeamAssignmentsTextBlock.Text = "Importe as respostas da etapa de aprovação para aprovar as causas.";
			ClearAhpComputedData();
			RebuildAhpTableRows();
			RefreshReportsSection();
			return;
		}
		try
		{
			int parsedResponseCount;
			IReadOnlyList<Q1ParsedRow> parsedRows = ParseQ1TableRows(csvPath, BuildExpectedCauseLabels(), out parsedResponseCount);
			q1ParsedRows.AddRange(parsedRows);
			q1ResponseCount = parsedResponseCount;
			RebuildQ1TableRows();
			LoadAhpTableFromFile(csvPath);
			RebuildQ2TableRows();
			TeamAssignmentsTextBlock.Text = ((parsedRows.Count > 0) ? $"Respostas de aprovação carregadas com {parsedRows.Count} causas." : "Respostas de aprovação importadas, mas sem linhas válidas.");
			RefreshReportsSection();
		}
		catch (Exception ex)
		{
			TeamAssignmentsTextBlock.Text = "Erro ao ler as respostas de aprovação: " + ex.Message;
			q1ResponseCount = 0;
			ClearAhpComputedData();
			RebuildAhpTableRows("Erro ao calcular os pesos dos grupos a partir das respostas de aprovação.");
			RebuildQ2TableRows();
			RefreshReportsSection();
		}
	}

	private void RebuildQ1TableRows()
	{
		if (!isValidationUiReady)
		{
			return;
		}
		q1Rows.Clear();
		double medianThreshold = (double.IsNaN(ValidationMedianNumberBox.Value) ? 0.0 : ValidationMedianNumberBox.Value);
		double proportionThreshold = (double.IsNaN(ValidationProportionNumberBox.Value) ? 0.0 : ValidationProportionNumberBox.Value);
		ISet<int> selectedScaleValues = GetSelectedScaleValues();
		foreach (Q1ParsedRow row in q1ParsedRows)
		{
			(double Median, double Proportion) tuple = BuildQ1Metrics(row.Count1, row.Count2, row.Count3, row.Count4, row.Count5, selectedScaleValues);
			double median = tuple.Median;
			double proportion = tuple.Proportion;
			string status = ((median >= medianThreshold && proportion >= proportionThreshold) ? "Aprovada" : "Reprovada");
			q1Rows.Add(new Q1TableRow(row.Number, row.Cause, row.Value1, row.Value2, row.Value3, row.Value4, row.Value5, median.ToString("F0", CultureInfo.CurrentCulture), proportion.ToString("F0", CultureInfo.CurrentCulture) + "%", status));
		}
		RefreshReportsSection();
	}

	private void LoadQ2TableForCurrentProject()
	{
		if (currentProject == null)
		{
			q2Rows.Clear();
			q2Alternatives.Clear();
			q2ResponseCount = 0;
			TopsisStatusTextBlock.Text = string.Empty;
		}
		else
		{
			string csvPath = Path.Combine(currentProject.FolderPath, FinalAnswersCsvFileName);
			LoadQ2TableFromFile(csvPath);
		}
	}

	private void LoadQ2TableFromFile(string csvPath)
	{
		q2Rows.Clear();
		q2Alternatives.Clear();
		q2ResponseCount = 0;
		if (!File.Exists(csvPath))
		{
			TopsisStatusTextBlock.Text = "Importe as respostas finais para montar o ranking.";
			RefreshReportsSection();
			return;
		}
		try
		{
			int responseCount;
			IReadOnlyList<TopsisAlternativeData> alternatives = ParseQ2DecisionMatrix(csvPath, groups.Select((NamedListItem group) => group.Name).ToList(), groups.Select((NamedListItem group) => group.ItemId).ToList(), out responseCount);
			q2Alternatives.AddRange(alternatives);
			q2ResponseCount = responseCount;
			RebuildQ2TableRows();
		}
		catch (Exception ex)
		{
			TopsisStatusTextBlock.Text = "Erro ao ler as respostas finais: " + ex.Message;
			RefreshReportsSection();
		}
	}

	private void RebuildQ2TableRows()
	{
		if (!isValidationUiReady)
		{
			return;
		}
		q2Rows.Clear();
		if (q2Alternatives.Count == 0)
		{
			TopsisStatusTextBlock.Text = "Respostas finais importadas, mas sem linhas válidas.";
			RefreshReportsSection();
			return;
		}
		if (groups.Count == 0)
		{
			TopsisStatusTextBlock.Text = "Cadastre grupos participantes antes de calcular o ranking final.";
			RefreshReportsSection();
			return;
		}
		int criteriaCount = groups.Count;
		double[,] decisionMatrix = new double[q2Alternatives.Count, criteriaCount];
		for (int i = 0; i < q2Alternatives.Count; i++)
		{
			for (int j = 0; j < criteriaCount; j++)
			{
				decisionMatrix[i, j] = ((j < q2Alternatives[i].CriteriaValues.Length) ? q2Alternatives[i].CriteriaValues[j] : 0.0);
			}
		}
		if (!TryBuildTopsisCriteriaWeights(criteriaCount, out double[] weights))
		{
			TopsisStatusTextBlock.Text = $"Respostas finais carregadas com {q2ResponseCount} respostas e {q2Alternatives.Count} causas, mas ainda faltam os pesos dos grupos. " + "Importe as respostas de aprovação para calcular os pesos.";
			RefreshReportsSection();
			return;
		}
		double[,] weightedNormalizedMatrix = new double[q2Alternatives.Count, criteriaCount];
		double[] normDenominator = new double[criteriaCount];
		for (int k = 0; k < criteriaCount; k++)
		{
			double sumSquares = 0.0;
			for (int l = 0; l < q2Alternatives.Count; l++)
			{
				sumSquares += decisionMatrix[l, k] * decisionMatrix[l, k];
			}
			normDenominator[k] = Math.Sqrt(sumSquares);
		}
		for (int m = 0; m < q2Alternatives.Count; m++)
		{
			for (int n = 0; n < criteriaCount; n++)
			{
				double normalized = ((normDenominator[n] > 0.0) ? (decisionMatrix[m, n] / normDenominator[n]) : 0.0);
				weightedNormalizedMatrix[m, n] = normalized * weights[n];
			}
		}
		double[] idealBest = new double[criteriaCount];
		double[] idealWorst = new double[criteriaCount];
		for (int num = 0; num < criteriaCount; num++)
		{
			double maxValue = double.NegativeInfinity;
			double minValue = double.PositiveInfinity;
			for (int num2 = 0; num2 < q2Alternatives.Count; num2++)
			{
				double value = weightedNormalizedMatrix[num2, num];
				if (value > maxValue)
				{
					maxValue = value;
				}
				if (value < minValue)
				{
					minValue = value;
				}
			}
			idealBest[num] = (double.IsFinite(maxValue) ? maxValue : 0.0);
			idealWorst[num] = (double.IsFinite(minValue) ? minValue : 0.0);
		}
		List<(TopsisAlternativeData, double)> rankedRows = new List<(TopsisAlternativeData, double)>(q2Alternatives.Count);
		for (int num3 = 0; num3 < q2Alternatives.Count; num3++)
		{
			double distanceToBest = 0.0;
			double distanceToWorst = 0.0;
			for (int num4 = 0; num4 < criteriaCount; num4++)
			{
				double num5 = weightedNormalizedMatrix[num3, num4];
				double deltaBest = num5 - idealBest[num4];
				double deltaWorst = num5 - idealWorst[num4];
				distanceToBest += deltaBest * deltaBest;
				distanceToWorst += deltaWorst * deltaWorst;
			}
			distanceToBest = Math.Sqrt(distanceToBest);
			distanceToWorst = Math.Sqrt(distanceToWorst);
			double denominator = distanceToBest + distanceToWorst;
			double closeness = ((denominator > 0.0) ? (distanceToWorst / denominator) : 0.0);
			rankedRows.Add((q2Alternatives[num3], closeness));
		}
		rankedRows = rankedRows.OrderByDescending<(TopsisAlternativeData, double), double>(((TopsisAlternativeData Alternative, double Score) item) => item.Score).ThenBy<(TopsisAlternativeData, double), string>(((TopsisAlternativeData Alternative, double Score) item) => item.Alternative.Id, StringComparer.OrdinalIgnoreCase).ToList();
		for (int i2 = 0; i2 < rankedRows.Count; i2++)
		{
			(TopsisAlternativeData, double) ranked = rankedRows[i2];
			q2Rows.Add(new TopsisTableRow(ranked.Item1.Id, ranked.Item1.Cause, (i2 + 1).ToString(CultureInfo.InvariantCulture)));
		}
		TopsisStatusTextBlock.Text = $"Ranking final calculado com os pesos dos grupos ({q2Alternatives.Count} causas).";
		RefreshReportsSection();
	}

	private bool TryBuildTopsisCriteriaWeights(int criteriaCount, out double[] weights)
	{
		weights = Array.Empty<double>();
		if (criteriaCount <= 0)
		{
			return false;
		}
		if (!HasAhpComputedDataForCurrentGroups())
		{
			return false;
		}
		weights = new double[criteriaCount];
		double sum = 0.0;
		for (int i = 0; i < criteriaCount; i++)
		{
			if (!ahpWeightsByGroupId.TryGetValue(groups[i].ItemId, out var weight) || weight < 0.0 || double.IsNaN(weight) || double.IsInfinity(weight))
			{
				weights = Array.Empty<double>();
				return false;
			}
			weights[i] = weight;
			sum += weight;
		}
		if (sum <= 0.0)
		{
			weights = Array.Empty<double>();
			return false;
		}
		for (int j = 0; j < criteriaCount; j++)
		{
			weights[j] /= sum;
		}
		return true;
	}

	private void LoadAhpTableFromFile(string csvPath)
	{
		ClearAhpComputedData();
		if (!File.Exists(csvPath) || groups.Count < 2)
		{
			RebuildAhpTableRows();
			return;
		}
		List<string> groupNames = groups.Select((NamedListItem group) => group.Name.Trim()).ToList();
		List<string> groupIds = groups.Select((NamedListItem group) => group.ItemId.Trim()).ToList();
		if (!TryParseAhpFromFormsCsv(csvPath, groupNames, groupIds, out AhpComputationResult result) || result == null)
		{
			RebuildAhpTableRows("Respostas de aprovação sem comparações válidas para calcular os pesos.");
			return;
		}
		for (int i = 0; i < groups.Count; i++)
		{
			ahpWeightsByGroupId[groups[i].ItemId] = result.Weights[i];
		}
		ahpCrPercent = result.CrPercent;
		ahpResponseCount = result.ResponseCount;
		RebuildAhpTableRows($"Pesos dos grupos calculados com {result.ResponseCount} respostas.");
	}

	private void ClearAhpComputedData()
	{
		ahpWeightsByGroupId.Clear();
		ahpCrPercent = null;
		ahpResponseCount = 0;
	}

	private static bool TryParseAhpFromFormsCsv(string csvPath, IReadOnlyList<string> groupNames, IReadOnlyList<string> groupIds, out AhpComputationResult? result)
	{
		result = null;
		if (groupNames.Count < 2)
		{
			return false;
		}
		List<string> normalizedGroupIds = ((groupIds.Count == groupNames.Count) ? groupIds.Select((string groupId) => groupId.Trim()).ToList() : (from index in Enumerable.Range(1, groupNames.Count)
			select $"G{index:00}").ToList());
		IReadOnlyList<IReadOnlyList<string>> records = ReadCsvRecords(csvPath);
		if (records.Count < 2)
		{
			return false;
		}
		IReadOnlyList<string> headerColumns = records[0];
		List<AhpPairDefinition> pairDefinitions = BuildAhpPairDefinitions(groupNames.Count);
		if (MapAhpPairColumnsByPosition(records, headerColumns, pairDefinitions, groupNames, normalizedGroupIds) == 0)
		{
			return false;
		}
		List<List<double>> pairValues = pairDefinitions.Select((AhpPairDefinition _) => new List<double>()).ToList();
		int parsedResponseCount = 0;
		for (int rowIndex = 1; rowIndex < records.Count; rowIndex++)
		{
			IReadOnlyList<string> row = records[rowIndex];
			if (row.Count == 0)
			{
				continue;
			}
			bool rowUsed = false;
			foreach (var (definition, pairIndex) in pairDefinitions.Select((AhpPairDefinition item, int index) => (item: item, index: index)))
			{
				if (definition.ColumnA >= 0 && definition.ColumnB >= 0 && definition.ColumnA < row.Count && definition.ColumnB < row.Count)
				{
					string comparisonAnswer = row[definition.ColumnA];
					string answerB = row[definition.ColumnB];
					if (TryBuildSaatyPairValue(comparisonAnswer, answerB, groupNames[definition.GroupAIndex], groupNames[definition.GroupBIndex], normalizedGroupIds[definition.GroupAIndex], normalizedGroupIds[definition.GroupBIndex], out var pairValue))
					{
						pairValues[pairIndex].Add(pairValue);
						rowUsed = true;
					}
				}
			}
			if (rowUsed)
			{
				parsedResponseCount++;
			}
		}
		if (parsedResponseCount == 0)
		{
			return false;
		}
		int groupCount = groupNames.Count;
		double[,] matrix = new double[groupCount, groupCount];
		for (int i = 0; i < groupCount; i++)
		{
			matrix[i, i] = 1.0;
		}
		foreach (var item in pairDefinitions.Select((AhpPairDefinition item, int index) => (item: item, index: index)))
		{
			AhpPairDefinition definition2 = item.item;
			int pairIndex2 = item.index;
			List<double> pairList = pairValues[pairIndex2];
			if (pairList.Count == 0)
			{
				matrix[definition2.GroupAIndex, definition2.GroupBIndex] = 1.0;
				matrix[definition2.GroupBIndex, definition2.GroupAIndex] = 1.0;
				continue;
			}
			double pairGeometricMean = Math.Exp(pairList.Average((double value) => Math.Log(value)));
			matrix[definition2.GroupAIndex, definition2.GroupBIndex] = pairGeometricMean;
			matrix[definition2.GroupBIndex, definition2.GroupAIndex] = 1.0 / pairGeometricMean;
		}
		double[] rowGeometricMeans = new double[groupCount];
		for (int i2 = 0; i2 < groupCount; i2++)
		{
			double product = 1.0;
			for (int j = 0; j < groupCount; j++)
			{
				product *= matrix[i2, j];
			}
			rowGeometricMeans[i2] = Math.Pow(product, 1.0 / (double)groupCount);
		}
		double totalGeometricMeans = rowGeometricMeans.Sum();
		if (totalGeometricMeans <= 0.0)
		{
			return false;
		}
		double[] weights = rowGeometricMeans.Select((double value) => value / totalGeometricMeans).ToArray();
		double[] aw = new double[groupCount];
		for (int i3 = 0; i3 < groupCount; i3++)
		{
			for (int j2 = 0; j2 < groupCount; j2++)
			{
				aw[i3] += matrix[i3, j2] * weights[j2];
			}
		}
		double lambdaMax = 0.0;
		int lambdaCount = 0;
		for (int i4 = 0; i4 < groupCount; i4++)
		{
			if (!(weights[i4] <= 0.0))
			{
				lambdaMax += aw[i4] / weights[i4];
				lambdaCount++;
			}
		}
		lambdaMax = ((lambdaCount <= 0) ? ((double)groupCount) : (lambdaMax / (double)lambdaCount));
		double ci = ((groupCount > 1) ? ((lambdaMax - (double)groupCount) / ((double)groupCount - 1.0)) : 0.0);
		ci = Math.Max(0.0, ci);
		double ri = GetAhpRandomIndex(groupCount);
		double cr = ((ri > 0.0) ? (ci / ri) : 0.0);
		if (double.IsNaN(cr) || double.IsInfinity(cr) || cr < 0.0)
		{
			cr = 0.0;
		}
		result = new AhpComputationResult(weights, cr * 100.0, parsedResponseCount);
		return true;
	}

	private static List<AhpPairDefinition> BuildAhpPairDefinitions(int groupCount)
	{
		List<AhpPairDefinition> definitions = new List<AhpPairDefinition>();
		int pairNumber = 1;
		for (int i = 0; i < groupCount; i++)
		{
			for (int j = i + 1; j < groupCount; j++)
			{
				definitions.Add(new AhpPairDefinition(i, j, pairNumber));
				pairNumber++;
			}
		}
		return definitions;
	}

	private static int MapAhpPairColumnsByPosition(IReadOnlyList<IReadOnlyList<string>> records, IReadOnlyList<string> headerColumns, List<AhpPairDefinition> pairDefinitions, IReadOnlyList<string> groupNames, IReadOnlyList<string> groupIds)
	{
		int lastCauseColumnIndex = -1;
		for (int columnIndex = 0; columnIndex < headerColumns.Count; columnIndex++)
		{
			if (NormalizeComparisonValue(headerColumns[columnIndex]).Contains("causa da evasao", StringComparison.Ordinal))
			{
				lastCauseColumnIndex = columnIndex;
			}
		}
		int searchColumnStart = Math.Max(0, lastCauseColumnIndex + 1);
		int matchedPairColumns = 0;
		for (int pairIndex = 0; pairIndex < pairDefinitions.Count; pairIndex++)
		{
			AhpPairDefinition definition = pairDefinitions[pairIndex];
			string groupA = groupNames[definition.GroupAIndex];
			string groupB = groupNames[definition.GroupBIndex];
			string groupAId = groupIds[definition.GroupAIndex];
			string groupBId = groupIds[definition.GroupBIndex];
			bool pairFound = false;
			for (int columnA = searchColumnStart; columnA < headerColumns.Count - 1; columnA++)
			{
				int columnB = columnA + 1;
				if (IsLikelyAhpColumnPair(records, columnA, columnB, groupA, groupB, groupAId, groupBId))
				{
					definition.ColumnA = columnA;
					definition.ColumnB = columnB;
					pairDefinitions[pairIndex] = definition;
					matchedPairColumns++;
					searchColumnStart = columnB + 1;
					pairFound = true;
					break;
				}
			}
			if (!pairFound)
			{
				break;
			}
		}
		return matchedPairColumns;
	}

	private static bool IsLikelyAhpColumnPair(IReadOnlyList<IReadOnlyList<string>> records, int columnA, int columnB, string groupA, string groupB, string groupAId, string groupBId)
	{
		int validRows = 0;
		int invalidRows = 0;
		for (int rowIndex = 1; rowIndex < records.Count; rowIndex++)
		{
			IReadOnlyList<string> row = records[rowIndex];
			if (columnA >= row.Count || columnB >= row.Count)
			{
				continue;
			}
			string answerA = row[columnA];
			string answerB = row[columnB];
			if (!string.IsNullOrWhiteSpace(answerA) || !string.IsNullOrWhiteSpace(answerB))
			{
				if (TryBuildSaatyPairValue(answerA, answerB, groupA, groupB, groupAId, groupBId, out var _))
				{
					validRows++;
				}
				else
				{
					invalidRows++;
				}
			}
		}
		if (validRows == 0)
		{
			return false;
		}
		return validRows >= invalidRows;
	}

	private static bool TryBuildSaatyPairValue(string comparisonAnswer, string intensityAnswer, string groupA, string groupB, string groupAId, string groupBId, out double value)
	{
		value = 0.0;
		int intensity = ParseSaatyIntensity(intensityAnswer);
		if ((intensity < 1 || intensity > 9) ? true : false)
		{
			return false;
		}
		string normalizedComparisonAnswer = NormalizeComparisonValue(comparisonAnswer);
		if (string.IsNullOrWhiteSpace(normalizedComparisonAnswer))
		{
			return false;
		}
		if (normalizedComparisonAnswer.Contains("igual", StringComparison.Ordinal))
		{
			value = 1.0;
			return true;
		}
		string normalizedGroupA = NormalizeComparisonValue(groupA);
		string normalizedGroupB = NormalizeComparisonValue(groupB);
		string normalizedGroupAId = NormalizeComparisonValue(groupAId);
		string normalizedGroupBId = NormalizeComparisonValue(groupBId);
		if (string.Equals(normalizedComparisonAnswer, normalizedGroupA, StringComparison.Ordinal))
		{
			value = intensity;
			return true;
		}
		if (string.Equals(normalizedComparisonAnswer, normalizedGroupB, StringComparison.Ordinal))
		{
			value = 1.0 / (double)intensity;
			return true;
		}
		if (string.Equals(normalizedComparisonAnswer, normalizedGroupAId, StringComparison.Ordinal))
		{
			value = intensity;
			return true;
		}
		if (string.Equals(normalizedComparisonAnswer, normalizedGroupBId, StringComparison.Ordinal))
		{
			value = 1.0 / (double)intensity;
			return true;
		}
		return false;
	}

	private static int ParseSaatyIntensity(string value)
	{
		string normalized = value.Trim();
		if (string.IsNullOrWhiteSpace(normalized))
		{
			return 0;
		}
		if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out var directValue) && directValue >= 1 && directValue <= 9)
		{
			return directValue;
		}
		string text = normalized;
		foreach (char character in text)
		{
			if (character >= '1' && character <= '9')
			{
				return character - 48;
			}
		}
		return 0;
	}

	private static IReadOnlyList<IReadOnlyList<string>> ReadCsvRecords(string csvPath)
	{
		string content = TryFixMojibake(ReadCsvTextWithEncodingFallback(csvPath));
		char separator = DetectCsvSeparator(content);
		return ParseCsvRecords(content, separator);
	}

	private static string ReadCsvTextWithEncodingFallback(string csvPath)
	{
		byte[] bytes = File.ReadAllBytes(csvPath);
		if (bytes.Length == 0)
		{
			return string.Empty;
		}
		try
		{
			return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true).GetString(bytes);
		}
		catch (DecoderFallbackException)
		{
			try
			{
				return Encoding.GetEncoding(1252).GetString(bytes);
			}
			catch
			{
				return Encoding.Latin1.GetString(bytes);
			}
		}
	}

	private static string TryFixMojibake(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return value;
		}
		int originalScore = GetMojibakeScore(value);
		if (originalScore == 0)
		{
			return value;
		}
		string fixedValue;
		try
		{
			byte[] bytes = Encoding.GetEncoding(1252).GetBytes(value);
			fixedValue = Encoding.UTF8.GetString(bytes);
		}
		catch
		{
			byte[] bytes2 = Encoding.Latin1.GetBytes(value);
			fixedValue = Encoding.UTF8.GetString(bytes2);
		}
		if (GetMojibakeScore(fixedValue) >= originalScore)
		{
			return value;
		}
		return fixedValue;
	}

	private static int GetMojibakeScore(string value)
	{
		int score = 0;
		for (int i = 0; i < value.Length; i++)
		{
			bool flag;
			switch (value[i])
			{
			case 'Â':
			case 'Ã':
			case 'â':
			case '\ufffd':
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			if (flag)
			{
				score++;
			}
		}
		return score;
	}

	private static IReadOnlyList<IReadOnlyList<string>> ParseCsvRecords(string content, char separator)
	{
		List<IReadOnlyList<string>> records = new List<IReadOnlyList<string>>();
		List<string> currentRow = new List<string>();
		StringBuilder currentField = new StringBuilder();
		bool insideQuotes = false;
		for (int i = 0; i < content.Length; i++)
		{
			char currentCharacter = content[i];
			if (currentCharacter == '"')
			{
				if (insideQuotes && i + 1 < content.Length && content[i + 1] == '"')
				{
					currentField.Append('"');
					i++;
				}
				else
				{
					insideQuotes = !insideQuotes;
				}
			}
			else if (currentCharacter == separator && !insideQuotes)
			{
				currentRow.Add(currentField.ToString().Trim());
				currentField.Clear();
			}
			else if ((currentCharacter == '\n' || currentCharacter == '\r') && !insideQuotes)
			{
				currentRow.Add(currentField.ToString().Trim());
				currentField.Clear();
				TrimTrailingEmptyColumns(currentRow);
				if (currentRow.Count > 0)
				{
					records.Add(new List<string>(currentRow));
				}
				currentRow.Clear();
				if (currentCharacter == '\r' && i + 1 < content.Length && content[i + 1] == '\n')
				{
					i++;
				}
			}
			else
			{
				currentField.Append(currentCharacter);
			}
		}
		currentRow.Add(currentField.ToString().Trim());
		TrimTrailingEmptyColumns(currentRow);
		if (currentRow.Count > 0)
		{
			records.Add(new List<string>(currentRow));
		}
		return records;
	}

	private static string NormalizeComparisonValue(string value)
	{
		string input = value.Trim().TrimStart('\ufeff');
		if (string.IsNullOrWhiteSpace(input))
		{
			return string.Empty;
		}
		string text = input.Normalize(NormalizationForm.FormD);
		StringBuilder builder = new StringBuilder(text.Length);
		bool previousWasSpace = false;
		string text2 = text;
		foreach (char character in text2)
		{
			if (CharUnicodeInfo.GetUnicodeCategory(character) == UnicodeCategory.NonSpacingMark)
			{
				continue;
			}
			char lowerCharacter = char.ToLowerInvariant(character);
			if (char.IsWhiteSpace(lowerCharacter))
			{
				if (!previousWasSpace)
				{
					builder.Append(' ');
					previousWasSpace = true;
				}
			}
			else
			{
				builder.Append(lowerCharacter);
				previousWasSpace = false;
			}
		}
		return builder.ToString().Normalize(NormalizationForm.FormC).Trim();
	}

	private static double GetAhpRandomIndex(int matrixSize)
	{
		if (matrixSize > 2)
		{
			return matrixSize switch
			{
				3 => 0.58, 
				4 => 0.9, 
				5 => 1.12, 
				6 => 1.24, 
				7 => 1.32, 
				8 => 1.41, 
				9 => 1.45, 
				10 => 1.49, 
				_ => 1.49, 
			};
		}
		return 0.0;
	}

	private HashSet<int> GetSelectedScaleValues()
	{
		HashSet<int> values = new HashSet<int>();
		if (Scale1CheckBox.IsChecked == true)
		{
			values.Add(1);
		}
		if (Scale2CheckBox.IsChecked == true)
		{
			values.Add(2);
		}
		if (Scale3CheckBox.IsChecked == true)
		{
			values.Add(3);
		}
		if (Scale4CheckBox.IsChecked == true)
		{
			values.Add(4);
		}
		if (Scale5CheckBox.IsChecked == true)
		{
			values.Add(5);
		}
		return values;
	}

	private static IReadOnlyList<Q1ParsedRow> ParseQ1TableRows(string csvPath, IReadOnlyList<string> expectedCauseLabels, out int responseCount)
	{
		responseCount = 0;
		IReadOnlyList<IReadOnlyList<string>> records = ReadCsvRecords(csvPath);
		if (records.Count == 0)
		{
			return Array.Empty<Q1ParsedRow>();
		}
		List<string> firstColumns = records[0].ToList();
		TrimTrailingEmptyColumns(firstColumns);
		int formsResponseCount;
		IReadOnlyList<Q1ParsedRow> formsRows = ParseFormsIntensityRows(records, expectedCauseLabels, out formsResponseCount);
		if (formsRows.Count > 0)
		{
			responseCount = formsResponseCount;
			return formsRows;
		}
		if (IsQ1Header(firstColumns))
		{
			IReadOnlyList<Q1ParsedRow> oldRows = ParseOldQ1Rows(records, 1);
			responseCount = EstimateQ1ResponseCountFromAggregatedRows(oldRows);
			return oldRows;
		}
		if (IsLikertRowFormat(firstColumns))
		{
			IReadOnlyList<Q1ParsedRow> oldRows2 = ParseOldQ1Rows(records, 0);
			responseCount = EstimateQ1ResponseCountFromAggregatedRows(oldRows2);
			return oldRows2;
		}
		IReadOnlyList<Q1ParsedRow> transposedRows = ParseTransposedQ1Rows(records);
		responseCount = EstimateQ1ResponseCountFromAggregatedRows(transposedRows);
		return transposedRows;
	}

	private static IReadOnlyList<TopsisAlternativeData> ParseQ2DecisionMatrix(string csvPath, IReadOnlyList<string> groupNames, IReadOnlyList<string> groupIds, out int parsedResponseCount)
	{
		parsedResponseCount = 0;
		IReadOnlyList<IReadOnlyList<string>> records = ReadCsvRecords(csvPath);
		if (records.Count < 2)
		{
			return Array.Empty<TopsisAlternativeData>();
		}
		List<string> headerColumns = records[0].ToList();
		TrimTrailingEmptyColumns(headerColumns);
		if (headerColumns.Count == 0)
		{
			return Array.Empty<TopsisAlternativeData>();
		}
		int vinculoColumnIndex = -1;
		for (int columnIndex = 0; columnIndex < headerColumns.Count; columnIndex++)
		{
			if (NormalizeComparisonValue(headerColumns[columnIndex]).Contains("vinculo", StringComparison.Ordinal))
			{
				vinculoColumnIndex = columnIndex;
				break;
			}
		}
		if (vinculoColumnIndex < 0 || groupNames.Count == 0)
		{
			return Array.Empty<TopsisAlternativeData>();
		}
		string[] normalizedGroupNames = groupNames.Select(NormalizeComparisonValue).ToArray();
		string[] normalizedGroupIds = ((groupIds.Count == groupNames.Count) ? groupIds.Select(NormalizeComparisonValue).ToArray() : (from index in Enumerable.Range(1, groupNames.Count)
			select $"g{index:00}").ToArray());
		List<(int, string)> causeColumns = new List<(int, string)>();
		for (int columnIndex2 = 0; columnIndex2 < headerColumns.Count; columnIndex2++)
		{
			if (NormalizeComparisonValue(headerColumns[columnIndex2]).Contains("causa da evasao", StringComparison.Ordinal))
			{
				string causeLabel = ExtractCauseLabelFromFormsHeader(headerColumns[columnIndex2]);
				if (string.IsNullOrWhiteSpace(causeLabel))
				{
					causeLabel = $"C{columnIndex2 + 1}";
				}
				causeColumns.Add((columnIndex2, causeLabel));
			}
		}
		if (causeColumns.Count == 0)
		{
			return Array.Empty<TopsisAlternativeData>();
		}
		double[,] sumsByCauseAndGroup = new double[causeColumns.Count, groupNames.Count];
		int[,] countsByCauseAndGroup = new int[causeColumns.Count, groupNames.Count];
		for (int rowIndex = 1; rowIndex < records.Count; rowIndex++)
		{
			IReadOnlyList<string> row = records[rowIndex];
			if (row.Count == 0 || vinculoColumnIndex >= row.Count)
			{
				continue;
			}
			string normalizedVinculo = NormalizeComparisonValue(row[vinculoColumnIndex]);
			if (string.IsNullOrWhiteSpace(normalizedVinculo))
			{
				continue;
			}
			int groupIndex = -1;
			for (int j = 0; j < normalizedGroupNames.Length; j++)
			{
				string normalizedGroupName = normalizedGroupNames[j];
				string normalizedGroupId = normalizedGroupIds[j];
				if (!string.IsNullOrWhiteSpace(normalizedGroupId) && (string.Equals(normalizedVinculo, normalizedGroupId, StringComparison.Ordinal) || normalizedVinculo.StartsWith(normalizedGroupId + " ", StringComparison.Ordinal) || normalizedVinculo.StartsWith(normalizedGroupId + "=", StringComparison.Ordinal) || normalizedVinculo.StartsWith(normalizedGroupId + " -", StringComparison.Ordinal)))
				{
					groupIndex = j;
					break;
				}
				if (!string.IsNullOrWhiteSpace(normalizedGroupName) && (string.Equals(normalizedVinculo, normalizedGroupName, StringComparison.Ordinal) || normalizedVinculo.Contains(normalizedGroupName, StringComparison.Ordinal) || normalizedGroupName.Contains(normalizedVinculo, StringComparison.Ordinal)))
				{
					groupIndex = j;
					break;
				}
			}
			if (groupIndex < 0)
			{
				continue;
			}
			bool hasAnyLikertValue = false;
			for (int causeIndex = 0; causeIndex < causeColumns.Count; causeIndex++)
			{
				int columnIndex3 = causeColumns[causeIndex].Item1;
				if (columnIndex3 < row.Count)
				{
					switch (ParseLikertFromFormsAnswer(row[columnIndex3]))
					{
					case 1:
						sumsByCauseAndGroup[causeIndex, groupIndex] += 1.0;
						countsByCauseAndGroup[causeIndex, groupIndex]++;
						hasAnyLikertValue = true;
						break;
					case 2:
						sumsByCauseAndGroup[causeIndex, groupIndex] += 2.0;
						countsByCauseAndGroup[causeIndex, groupIndex]++;
						hasAnyLikertValue = true;
						break;
					case 3:
						sumsByCauseAndGroup[causeIndex, groupIndex] += 3.0;
						countsByCauseAndGroup[causeIndex, groupIndex]++;
						hasAnyLikertValue = true;
						break;
					case 4:
						sumsByCauseAndGroup[causeIndex, groupIndex] += 4.0;
						countsByCauseAndGroup[causeIndex, groupIndex]++;
						hasAnyLikertValue = true;
						break;
					case 5:
						sumsByCauseAndGroup[causeIndex, groupIndex] += 5.0;
						countsByCauseAndGroup[causeIndex, groupIndex]++;
						hasAnyLikertValue = true;
						break;
					}
				}
			}
			if (hasAnyLikertValue)
			{
				parsedResponseCount++;
			}
		}
		List<TopsisAlternativeData> rows = new List<TopsisAlternativeData>(causeColumns.Count);
		for (int causeIndex2 = 0; causeIndex2 < causeColumns.Count; causeIndex2++)
		{
			(string Number, string Cause) tuple = ParseCauseLabel(causeColumns[causeIndex2].Item2, causeIndex2 + 1);
			string number = tuple.Number;
			string cause = tuple.Cause;
			double[] criterionValues = new double[groupNames.Count];
			bool hasValue = false;
			for (int groupIndex2 = 0; groupIndex2 < groupNames.Count; groupIndex2++)
			{
				int count = countsByCauseAndGroup[causeIndex2, groupIndex2];
				if (count <= 0)
				{
					criterionValues[groupIndex2] = 0.0;
					continue;
				}
				criterionValues[groupIndex2] = sumsByCauseAndGroup[causeIndex2, groupIndex2] / (double)count;
				hasValue = true;
			}
			if (hasValue)
			{
				rows.Add(new TopsisAlternativeData(number, cause, criterionValues));
			}
		}
		return rows;
	}

	private static IReadOnlyList<Q1ParsedRow> ParseFormsIntensityRows(IReadOnlyList<IReadOnlyList<string>> records, IReadOnlyList<string> expectedCauseLabels, out int responseCount)
	{
		responseCount = 0;
		if (records.Count < 2 || expectedCauseLabels.Count == 0)
		{
			return Array.Empty<Q1ParsedRow>();
		}
		List<string> headerColumns = records[0].ToList();
		TrimTrailingEmptyColumns(headerColumns);
		if (headerColumns.Count == 0)
		{
			return Array.Empty<Q1ParsedRow>();
		}
		int[] causeColumnIndexes = new int[expectedCauseLabels.Count];
		Array.Fill(causeColumnIndexes, -1);
		int matchedColumns = 0;
		for (int causeIndex = 0; causeIndex < expectedCauseLabels.Count; causeIndex++)
		{
			string causeLabel = expectedCauseLabels[causeIndex];
			for (int columnIndex = 0; columnIndex < headerColumns.Count; columnIndex++)
			{
				if (IsFormsIntensityHeader(headerColumns[columnIndex], causeLabel))
				{
					causeColumnIndexes[causeIndex] = columnIndex;
					matchedColumns++;
					break;
				}
			}
		}
		if (matchedColumns == 0)
		{
			return Array.Empty<Q1ParsedRow>();
		}
		(double, double, double, double, double)[] countsByCause = new(double, double, double, double, double)[expectedCauseLabels.Count];
		for (int rowIndex = 1; rowIndex < records.Count; rowIndex++)
		{
			IReadOnlyList<string> row = records[rowIndex];
			if (row.Count == 0)
			{
				continue;
			}
			bool rowUsed = false;
			for (int i = 0; i < expectedCauseLabels.Count; i++)
			{
				int columnIndex2 = causeColumnIndexes[i];
				if (columnIndex2 >= 0 && columnIndex2 < row.Count)
				{
					switch (ParseLikertFromFormsAnswer(row[columnIndex2]))
					{
					case 1:
						countsByCause[i].Item1 += 1.0;
						rowUsed = true;
						break;
					case 2:
						countsByCause[i].Item2 += 1.0;
						rowUsed = true;
						break;
					case 3:
						countsByCause[i].Item3 += 1.0;
						rowUsed = true;
						break;
					case 4:
						countsByCause[i].Item4 += 1.0;
						rowUsed = true;
						break;
					case 5:
						countsByCause[i].Item5 += 1.0;
						rowUsed = true;
						break;
					}
				}
			}
			if (rowUsed)
			{
				responseCount++;
			}
		}
		List<Q1ParsedRow> rows = new List<Q1ParsedRow>(expectedCauseLabels.Count);
		for (int j = 0; j < expectedCauseLabels.Count; j++)
		{
			(double, double, double, double, double) counts = countsByCause[j];
			string sourceLabel = expectedCauseLabels[j];
			int sourceColumnIndex = causeColumnIndexes[j];
			if (sourceColumnIndex >= 0 && sourceColumnIndex < headerColumns.Count)
			{
				string extractedLabel = ExtractCauseLabelFromFormsHeader(headerColumns[sourceColumnIndex]);
				if (!string.IsNullOrWhiteSpace(extractedLabel))
				{
					sourceLabel = extractedLabel;
				}
			}
			var (number, cause) = ParseCauseLabel(sourceLabel, j + 1);
			rows.Add(new Q1ParsedRow(number, cause, FormatCount(counts.Item1), FormatCount(counts.Item2), FormatCount(counts.Item3), FormatCount(counts.Item4), FormatCount(counts.Item5), counts.Item1, counts.Item2, counts.Item3, counts.Item4, counts.Item5));
		}
		return rows;
	}

	private static int EstimateQ1ResponseCountFromAggregatedRows(IReadOnlyList<Q1ParsedRow> rows)
	{
		List<double> totals = (from row in rows
			select row.Count1 + row.Count2 + row.Count3 + row.Count4 + row.Count5 into total
			where total > 0.0
			orderby total
			select total).ToList();
		if (totals.Count == 0)
		{
			return 0;
		}
		double median = ((totals.Count % 2 == 1) ? totals[totals.Count / 2] : ((totals[totals.Count / 2 - 1] + totals[totals.Count / 2]) / 2.0));
		return Math.Max(0, (int)Math.Round(median, MidpointRounding.AwayFromZero));
	}

	private static IReadOnlyList<Q1ParsedRow> ParseOldQ1Rows(IReadOnlyList<IReadOnlyList<string>> records, int lineIndex)
	{
		List<Q1ParsedRow> rows = new List<Q1ParsedRow>();
		for (int i = lineIndex; i < records.Count; i++)
		{
			IReadOnlyList<string> record = records[i];
			if (record.Count != 0)
			{
				List<string> columns = record.ToList();
				TrimTrailingEmptyColumns(columns);
				if (columns.Count >= 7)
				{
					double count1 = ParseNumeric(columns[2]);
					double count2 = ParseNumeric(columns[3]);
					double count3 = ParseNumeric(columns[4]);
					double count4 = ParseNumeric(columns[5]);
					double count5 = ParseNumeric(columns[6]);
					rows.Add(new Q1ParsedRow(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5], columns[6], count1, count2, count3, count4, count5));
				}
			}
		}
		return rows;
	}

	private static IReadOnlyList<Q1ParsedRow> ParseTransposedQ1Rows(IReadOnlyList<IReadOnlyList<string>> records)
	{
		List<string> causes = records[0].ToList();
		TrimTrailingEmptyColumns(causes);
		causes = (from text in causes
			select text.Trim().TrimStart('\ufeff') into value
			where !string.IsNullOrWhiteSpace(value)
			select value).ToList();
		if (causes.Count == 0)
		{
			return Array.Empty<Q1ParsedRow>();
		}
		(double, double, double, double, double)[] countsByCause = new(double, double, double, double, double)[causes.Count];
		for (int rowIndex = 1; rowIndex < records.Count; rowIndex++)
		{
			IReadOnlyList<string> record = records[rowIndex];
			if (record.Count == 0)
			{
				continue;
			}
			List<string> values = record.ToList();
			TrimTrailingEmptyColumns(values);
			int maxColumn = Math.Min(causes.Count, values.Count);
			for (int causeIndex = 0; causeIndex < maxColumn; causeIndex++)
			{
				switch ((int)Math.Round(ParseNumeric(values[causeIndex]), MidpointRounding.AwayFromZero))
				{
				case 1:
					countsByCause[causeIndex].Item1 += 1.0;
					break;
				case 2:
					countsByCause[causeIndex].Item2 += 1.0;
					break;
				case 3:
					countsByCause[causeIndex].Item3 += 1.0;
					break;
				case 4:
					countsByCause[causeIndex].Item4 += 1.0;
					break;
				case 5:
					countsByCause[causeIndex].Item5 += 1.0;
					break;
				}
			}
		}
		List<Q1ParsedRow> rows = new List<Q1ParsedRow>(causes.Count);
		for (int i = 0; i < causes.Count; i++)
		{
			(double, double, double, double, double) counts = countsByCause[i];
			var (number, cause) = ParseCauseLabel(causes[i], i + 1);
			rows.Add(new Q1ParsedRow(number, cause, FormatCount(counts.Item1), FormatCount(counts.Item2), FormatCount(counts.Item3), FormatCount(counts.Item4), FormatCount(counts.Item5), counts.Item1, counts.Item2, counts.Item3, counts.Item4, counts.Item5));
		}
		return rows;
	}

	private static bool IsLikertRowFormat(IReadOnlyList<string> columns)
	{
		if (columns.Count < 7)
		{
			return false;
		}
		if (TryParseNumeric(columns[2], out var parsed) && TryParseNumeric(columns[3], out parsed) && TryParseNumeric(columns[4], out parsed) && TryParseNumeric(columns[5], out parsed))
		{
			return TryParseNumeric(columns[6], out parsed);
		}
		return false;
	}

	private static void TrimTrailingEmptyColumns(List<string> columns)
	{
		int i = columns.Count - 1;
		while (i >= 0 && string.IsNullOrWhiteSpace(columns[i]))
		{
			columns.RemoveAt(i);
			i--;
		}
	}

	private static string FormatCount(double value)
	{
		if (Math.Abs(value - Math.Round(value)) < 0.0001)
		{
			return ((int)Math.Round(value)).ToString(CultureInfo.InvariantCulture);
		}
		return value.ToString("F1", CultureInfo.CurrentCulture);
	}

	private static (double Median, double Proportion) BuildQ1Metrics(double v1, double v2, double v3, double v4, double v5, ISet<int> selectedScaleValues)
	{
		double total = v1 + v2 + v3 + v4 + v5;
		if (total <= 0.0)
		{
			return (Median: 0.0, Proportion: 0.0);
		}
		double[] counts = new double[5] { v1, v2, v3, v4, v5 };
		double selectedTotal = 0.0;
		if (selectedScaleValues.Contains(1))
		{
			selectedTotal += v1;
		}
		if (selectedScaleValues.Contains(2))
		{
			selectedTotal += v2;
		}
		if (selectedScaleValues.Contains(3))
		{
			selectedTotal += v3;
		}
		if (selectedScaleValues.Contains(4))
		{
			selectedTotal += v4;
		}
		if (selectedScaleValues.Contains(5))
		{
			selectedTotal += v5;
		}
		double median = ComputeWeightedLikertMedian(counts, total);
		double proportion = selectedTotal / total * 100.0;
		return (Median: median, Proportion: proportion);
	}

	private static double ComputeWeightedLikertMedian(IReadOnlyList<double> counts, double total)
	{
		double roundedTotal = Math.Round(total, MidpointRounding.AwayFromZero);
		if (Math.Abs(total - roundedTotal) < 0.0001)
		{
			int observationCount = Math.Max(1, (int)roundedTotal);
			double lower = FindLikertValueAtPosition(counts, (observationCount + 1) / 2.0);
			if (observationCount % 2 == 1)
			{
				return lower;
			}
			double upper = FindLikertValueAtPosition(counts, observationCount / 2.0 + 1.0);
			return (lower + upper) / 2.0;
		}
		return FindLikertValueAtPosition(counts, total / 2.0);
	}

	private static double FindLikertValueAtPosition(IReadOnlyList<double> counts, double position)
	{
		double cumulative = 0.0;
		for (int i = 0; i < counts.Count; i++)
		{
			cumulative += counts[i];
			if (position <= cumulative)
			{
				return i + 1;
			}
		}
		return counts.Count;
	}

	private static double ParseNumeric(string value)
	{
		if (!TryParseNumeric(value, out var parsed))
		{
			return 0.0;
		}
		return parsed;
	}

	private static bool TryParseNumeric(string value, out double parsed)
	{
		string trimmed = value.Trim();
		if (double.TryParse(trimmed, NumberStyles.Float, CultureInfo.CurrentCulture, out parsed))
		{
			return true;
		}
		if (double.TryParse(trimmed, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
		{
			return true;
		}
		return double.TryParse(trimmed.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out parsed);
	}

	private static bool IsQ1Header(IReadOnlyList<string> columns)
	{
		if (columns.Count < 2)
		{
			return false;
		}
		string first = NormalizeHeaderName(columns[0]);
		string second = NormalizeHeaderName(columns[1]);
		if (string.Equals(first, "numero", StringComparison.OrdinalIgnoreCase) || string.Equals(first, "id", StringComparison.OrdinalIgnoreCase))
		{
			return string.Equals(second, "causa", StringComparison.OrdinalIgnoreCase);
		}
		return false;
	}

	private static bool IsFormsIntensityHeader(string header, string causeLabel)
	{
		string normalizedHeader = NormalizeHeaderForComparison(header);
		string normalizedCauseLabel = NormalizeHeaderForComparison(causeLabel);
		if (string.IsNullOrWhiteSpace(normalizedHeader) || string.IsNullOrWhiteSpace(normalizedCauseLabel))
		{
			return false;
		}
		if (!string.Equals(normalizedHeader, normalizedCauseLabel, StringComparison.OrdinalIgnoreCase))
		{
			return normalizedHeader.Contains(normalizedCauseLabel, StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	private static string NormalizeHeaderForComparison(string value)
	{
		string text = value.Trim().TrimStart('\ufeff');
		StringBuilder normalized = new StringBuilder(text.Length);
		bool previousWasSpace = false;
		string text2 = text;
		foreach (char character in text2)
		{
			if (char.IsWhiteSpace(character))
			{
				if (!previousWasSpace)
				{
					normalized.Append(' ');
					previousWasSpace = true;
				}
			}
			else
			{
				normalized.Append(character);
				previousWasSpace = false;
			}
		}
		return normalized.ToString().Trim();
	}

	private static int ParseLikertFromFormsAnswer(string value)
	{
		string normalized = value.Trim();
		if (string.IsNullOrWhiteSpace(normalized))
		{
			return 0;
		}
		if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out var directValue) && directValue >= 1 && directValue <= 5)
		{
			return directValue;
		}
		string text = normalized;
		foreach (char character in text)
		{
			if (character >= '1' && character <= '5')
			{
				return character - 48;
			}
		}
		return 0;
	}

	private static string ExtractCauseLabelFromFormsHeader(string header)
	{
		string normalized = header.Trim().TrimStart('\ufeff');
		if (string.IsNullOrWhiteSpace(normalized))
		{
			return string.Empty;
		}
		int startBracketIndex = normalized.IndexOf('[');
		int endBracketIndex = normalized.LastIndexOf(']');
		if (startBracketIndex >= 0 && endBracketIndex > startBracketIndex)
		{
			int num = startBracketIndex + 1;
			string insideBrackets = normalized.Substring(num, endBracketIndex - num).Trim();
			if (!string.IsNullOrWhiteSpace(insideBrackets))
			{
				return insideBrackets;
			}
		}
		int colonIndex = normalized.IndexOf(':');
		if (colonIndex >= 0 && colonIndex < normalized.Length - 1)
		{
			string text = normalized;
			int num = colonIndex + 1;
			string suffix = text.Substring(num, text.Length - num).Trim();
			if (!string.IsNullOrWhiteSpace(suffix))
			{
				return suffix;
			}
		}
		return normalized;
	}

	private static (string Number, string Cause) ParseCauseLabel(string label, int fallbackNumber)
	{
		string normalizedLabel = label.Trim().TrimStart('\ufeff');
		int separatorIndex = normalizedLabel.IndexOf('.');
		if (separatorIndex <= 0 || separatorIndex >= normalizedLabel.Length - 1)
		{
			return (Number: fallbackNumber.ToString(CultureInfo.InvariantCulture), Cause: normalizedLabel);
		}
		string prefix = normalizedLabel.Substring(0, separatorIndex).Trim();
		string text = normalizedLabel;
		int num = separatorIndex + 1;
		string cause = text.Substring(num, text.Length - num).Trim();
		string item = (string.IsNullOrWhiteSpace(prefix) ? fallbackNumber.ToString(CultureInfo.InvariantCulture) : prefix);
		if (string.IsNullOrWhiteSpace(cause))
		{
			cause = normalizedLabel;
		}
		return (Number: item, Cause: cause);
	}

	private static string NormalizeHeaderName(string value)
	{
		return value.Trim().TrimStart('\ufeff').Replace(" ", string.Empty);
	}

	private static char DetectCsvSeparator(string content)
	{
		if (string.IsNullOrWhiteSpace(content))
		{
			return ',';
		}
		int semicolonScore = ScoreSeparator(content, ';');
		int commaScore = ScoreSeparator(content, ',');
		if (semicolonScore == commaScore)
		{
			string? source = content.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
			int semicolons = source.Count((char character) => character == ';');
			int commas = source.Count((char character) => character == ',');
			if (semicolons < commas)
			{
				return ',';
			}
			return ';';
		}
		if (semicolonScore <= commaScore)
		{
			return ',';
		}
		return ';';
	}

	private static int ScoreSeparator(string content, char separator)
	{
		IReadOnlyList<IReadOnlyList<string>> records = ParseCsvRecords(content, separator);
		if (records.Count == 0)
		{
			return int.MinValue;
		}
		int headerCount = records[0].Count;
		if (headerCount <= 1)
		{
			return -1073741824;
		}
		int sampleCount = 0;
		int consistentCount = 0;
		int totalCount = 0;
		for (int i = 1; i < records.Count; i++)
		{
			if (sampleCount >= 30)
			{
				break;
			}
			int columnsCount = records[i].Count;
			if (columnsCount != 0)
			{
				sampleCount++;
				totalCount += columnsCount;
				if (columnsCount == headerCount)
				{
					consistentCount++;
				}
			}
		}
		string text = NormalizeComparisonValue(string.Join(" ", records[0]));
		int markerScore = 0;
		if (text.Contains("carimbo de data/hora", StringComparison.Ordinal))
		{
			markerScore += 4;
		}
		if (text.Contains("intensidade da causa", StringComparison.Ordinal))
		{
			markerScore += 4;
		}
		if (text.Contains("comparacao", StringComparison.Ordinal))
		{
			markerScore += 2;
		}
		int averageCount = ((sampleCount == 0) ? headerCount : (totalCount / sampleCount));
		int consistencyPenalty = ((sampleCount != 0) ? ((sampleCount - consistentCount) * 6) : 0);
		int spreadPenalty = Math.Abs(headerCount - averageCount);
		return headerCount * 20 + consistentCount * 40 + markerScore * 100 - consistencyPenalty - spreadPenalty;
	}

	private static List<string> SplitCsvLine(string line, char separator)
	{
		List<string> columns = new List<string>();
		StringBuilder field = new StringBuilder();
		bool insideQuotes = false;
		for (int i = 0; i < line.Length; i++)
		{
			char current = line[i];
			if (current == '"')
			{
				if (insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
				{
					field.Append('"');
					i++;
				}
				else
				{
					insideQuotes = !insideQuotes;
				}
			}
			else if (current == separator && !insideQuotes)
			{
				columns.Add(field.ToString().Trim());
				field.Clear();
			}
			else
			{
				field.Append(current);
			}
		}
		columns.Add(field.ToString().Trim());
		return columns;
	}

	private async void OnImportQ1CsvClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		string sourceCsvPath = await PickCsvFilePathAsync();
		if (string.IsNullOrWhiteSpace(sourceCsvPath))
		{
			return;
		}
		string destinationCsvPath = Path.Combine(currentProject.FolderPath, ValidationCsvFileName);
		try
		{
			Directory.CreateDirectory(currentProject.FolderPath);
			File.Copy(sourceCsvPath, destinationCsvPath, overwrite: true);
			LoadQ1TableFromFile(destinationCsvPath);
			MarkProjectAsModified();
			await ShowMessageAsync("Importação concluída", "Respostas de aprovação importadas para:\n" + destinationCsvPath);
		}
		catch (Exception ex)
		{
			await ShowMessageAsync("Erro ao importar", "Não foi possível importar as respostas de aprovação. Verifique se o CSV veio do questionário de validação das causas.\n\n" + ex.Message);
		}
	}

	private async void OnClearQ1TableClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null)
		{
			string contentText = ((q1Rows.Count > 0) ? $"Deseja realmente limpar a aprovação das causas ({q1Rows.Count} linhas)?" : "Deseja realmente limpar a aprovação das causas?");
			if (await new ContentDialog
			{
				Title = "Confirmar limpeza",
				Content = contentText,
				PrimaryButtonText = "Limpar",
				CloseButtonText = "Cancelar",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = base.XamlRoot
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				q1Rows.Clear();
				q1ParsedRows.Clear();
				q1ResponseCount = 0;
				TeamAssignmentsTextBlock.Text = "Aprovação das causas limpa.";
				ClearAhpComputedData();
				RebuildAhpTableRows("Pesos dos grupos limpos.");
				RebuildQ2TableRows();
			}
		}
	}

	private void OnValidationMedianValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
	{
		if (isValidationUiReady && !isUpdatingValidationMedian && !double.IsNaN(sender.Value))
		{
			double roundedValue = RoundValidationInteger(sender.Value, 1.0, 5.0);
			if (sender.Value.Equals(roundedValue))
			{
				RebuildQ1TableRows();
				return;
			}
			isUpdatingValidationMedian = true;
			sender.Value = roundedValue;
			isUpdatingValidationMedian = false;
			RebuildQ1TableRows();
		}
	}

	private void OnValidationMedianLostFocus(object sender, RoutedEventArgs e)
	{
		if (isValidationUiReady && sender is NumberBox numberBox && !double.IsNaN(numberBox.Value))
		{
			double roundedValue = RoundValidationInteger(numberBox.Value, 1.0, 5.0);
			isUpdatingValidationMedian = true;
			numberBox.Value = roundedValue;
			numberBox.Text = roundedValue.ToString("F0", CultureInfo.CurrentCulture);
			isUpdatingValidationMedian = false;
			RebuildQ1TableRows();
		}
	}

	private void OnValidationProportionValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
	{
		if (!isUpdatingValidationProportion && !double.IsNaN(sender.Value))
		{
			double roundedValue = RoundValidationInteger(sender.Value, 0.0, 100.0);
			if (!sender.Value.Equals(roundedValue))
			{
				isUpdatingValidationProportion = true;
				sender.Value = roundedValue;
				isUpdatingValidationProportion = false;
			}
			RebuildQ1TableRows();
		}
	}

	private void OnValidationProportionLostFocus(object sender, RoutedEventArgs e)
	{
		if (sender is NumberBox numberBox && !double.IsNaN(numberBox.Value))
		{
			double roundedValue = RoundValidationInteger(numberBox.Value, 0.0, 100.0);
			isUpdatingValidationProportion = true;
			numberBox.Value = roundedValue;
			numberBox.Text = roundedValue.ToString("F0", CultureInfo.CurrentCulture);
			isUpdatingValidationProportion = false;
			RebuildQ1TableRows();
		}
	}

	private void OnAhpConsistencyValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
	{
		if (isValidationUiReady && !isUpdatingAhpConsistency && !double.IsNaN(sender.Value))
		{
			double roundedValue = Math.Clamp(Math.Round(sender.Value, 1, MidpointRounding.AwayFromZero), 0.0, 100.0);
			if (!sender.Value.Equals(roundedValue))
			{
				isUpdatingAhpConsistency = true;
				sender.Value = roundedValue;
				isUpdatingAhpConsistency = false;
			}
			RebuildAhpTableRows();
		}
	}

	private void OnAhpConsistencyLostFocus(object sender, RoutedEventArgs e)
	{
		if (isValidationUiReady && sender is NumberBox numberBox && !double.IsNaN(numberBox.Value))
		{
			double roundedValue = Math.Clamp(Math.Round(numberBox.Value, 1, MidpointRounding.AwayFromZero), 0.0, 100.0);
			isUpdatingAhpConsistency = true;
			numberBox.Value = roundedValue;
			numberBox.Text = roundedValue.ToString("F1", CultureInfo.CurrentCulture);
			isUpdatingAhpConsistency = false;
			RebuildAhpTableRows();
		}
	}

	private void OnScaleSelectionChanged(object sender, RoutedEventArgs e)
	{
		if (isValidationUiReady)
		{
			RebuildQ1TableRows();
		}
	}

	private void OnPageBackgroundTapped(object sender, TappedRoutedEventArgs e)
	{
		if (e.OriginalSource is DependencyObject source && !HasAncestor<NumberBox>(source))
		{
			PageRootGrid.Focus(FocusState.Programmatic);
		}
	}

	private static bool HasAncestor<T>(DependencyObject source) where T : DependencyObject
	{
		DependencyObject current = source;
		while ((object)current != null)
		{
			if (current is T)
			{
				return true;
			}
			current = VisualTreeHelper.GetParent(current);
		}
		return false;
	}

	private async void OnExportQ1TxtClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		List<NamedListItem> groupItems = groups.Where((NamedListItem item) => !string.IsNullOrWhiteSpace(item.ItemId) && !string.IsNullOrWhiteSpace(item.Name)).ToList();
		List<string> groupNames = groupItems.Select((NamedListItem item) => item.Name.Trim()).ToList();
		List<string> groupIds = groupItems.Select((NamedListItem item) => item.ItemId.Trim()).ToList();
		if (groupNames.Count < 2)
		{
			await ShowMessageAsync("Participantes insuficientes", "Cadastre ao menos dois grupos participantes antes de gerar o questionário de validação.");
			return;
		}
		List<string> causeRows = (from item in causes
			where !string.IsNullOrWhiteSpace(item.Name)
			select item.ItemId + ". " + item.Name.Trim()).ToList();
		if (causeRows.Count < 2)
		{
			await ShowMessageAsync("Causas insuficientes", "Cadastre ao menos duas causas possíveis antes de gerar o questionário de validação.");
			return;
		}
		if ((object)App.MainWindowInstance == null)
		{
			await ShowMessageAsync("Erro", "Janela principal não encontrada para abrir seletor de arquivo.");
			return;
		}
		FileSavePicker obj = new FileSavePicker
		{
			SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
			SuggestedFileName = BuildSuggestedFileName("formulario_validacao_causas"),
			FileTypeChoices = { 
			{
				"Texto",
				(IList<string>)new List<string> { ".txt" }
			} }
		};
		InitializeWithWindow.Initialize(obj, WindowNative.GetWindowHandle(App.MainWindowInstance));
		StorageFile selectedFile = await obj.PickSaveFileAsync();
		if ((object)selectedFile != null)
		{
			string saveLocation = (string.IsNullOrWhiteSpace(selectedFile.Path) ? selectedFile.Name : selectedFile.Path);
			try
			{
				string institution = (currentProject.Name ?? string.Empty).Trim();
				string course = (currentProject.Course ?? string.Empty).Trim();
				string description = BuildQ1FormDescription(institution, course);
				string content = BuildFormsAppsScript(groupNames, groupIds, causeRows, institution, course, "QUESTIONÁRIO DE VALIDAÇÃO DAS CAUSAS\nE PESO DOS GRUPOS", description, null, includePairQuestions: true, includeAllGroupsInVinculoQuestion: true, useGroupCodesInVinculoQuestion: false, includeProfilePrincipalAtuacaoQuestion: true);
				await FileIO.WriteTextAsync(selectedFile, content);
				await ShowQ1ExportNextStepsDialogAsync(saveLocation, content);
			}
			catch (Exception ex)
			{
				await ShowMessageAsync("Erro ao gerar questionário", "Não foi possível salvar o arquivo do questionário de validação.\n\n" + ex.Message);
			}
		}
	}

	private async void OnExportQ2TxtClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		List<NamedListItem> groupItems = groups.Where((NamedListItem item) => !string.IsNullOrWhiteSpace(item.ItemId) && !string.IsNullOrWhiteSpace(item.Name)).ToList();
		List<string> groupNames = groupItems.Select((NamedListItem item) => item.Name.Trim()).ToList();
		List<string> groupIds = groupItems.Select((NamedListItem item) => item.ItemId.Trim()).ToList();
		if (groupNames.Count < 2)
		{
			await ShowMessageAsync("Participantes insuficientes", "Cadastre ao menos dois grupos participantes antes de gerar o questionário final.");
			return;
		}
		RebuildQ1TableRows();
		List<string> causeRows = BuildApprovedCauseLabelsForQ2();
		if (causeRows.Count < 2)
		{
			await ShowMessageAsync("Causas aprovadas insuficientes", "É necessário ter ao menos duas causas aprovadas para gerar o questionário final.");
			return;
		}
		if ((object)App.MainWindowInstance == null)
		{
			await ShowMessageAsync("Erro", "Janela principal não encontrada para abrir seletor de arquivo.");
			return;
		}
		FileSavePicker obj = new FileSavePicker
		{
			SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
			SuggestedFileName = BuildSuggestedFileName("formulario_ranking_final"),
			FileTypeChoices = { 
			{
				"Texto",
				(IList<string>)new List<string> { ".txt" }
			} }
		};
		InitializeWithWindow.Initialize(obj, WindowNative.GetWindowHandle(App.MainWindowInstance));
		StorageFile selectedFile = await obj.PickSaveFileAsync();
		if ((object)selectedFile != null)
		{
			string saveLocation = (string.IsNullOrWhiteSpace(selectedFile.Path) ? selectedFile.Name : selectedFile.Path);
			try
			{
				string institution = (currentProject.Name ?? string.Empty).Trim();
				string course = (currentProject.Course ?? string.Empty).Trim();
				string q2Description = "Prezado(a) participante,\n\nEste questionário ajuda a medir a intensidade das causas aprovadas na etapa anterior. Sua resposta é anônima e será usada para montar o ranking final de prioridades da escola ou curso. O tempo estimado de preenchimento é de 5 minutos.";
				string content = BuildFormsAppsScript(groupNames, groupIds, causeRows, institution, course, "QUESTIONÁRIO FINAL - RANKING DAS CAUSAS DE EVASÃO", q2Description, null, includePairQuestions: false, includeAllGroupsInVinculoQuestion: true, useGroupCodesInVinculoQuestion: false, includeProfilePrincipalAtuacaoQuestion: false);
				await FileIO.WriteTextAsync(selectedFile, content);
				await ShowQ2ExportNextStepsDialogAsync(saveLocation, content);
			}
			catch (Exception ex)
			{
				await ShowMessageAsync("Erro ao gerar questionário", "Não foi possível salvar o arquivo do questionário final.\n\n" + ex.Message);
			}
		}
	}

	private static string BuildFormsAppsScript(List<string> groupNames, List<string>? groupIds, List<string> causeRows, string institutionName, string courseName, string formTitle, string? formDescription, List<string>? vinculoGroupIdsFilter, bool includePairQuestions, bool includeAllGroupsInVinculoQuestion, bool useGroupCodesInVinculoQuestion, bool includeProfilePrincipalAtuacaoQuestion)
	{
		List<string> normalizedGroupIds = ((groupIds != null && groupIds.Count == groupNames.Count) ? groupIds.Select((string id) => id.Trim()).ToList() : (from index in Enumerable.Range(1, groupNames.Count)
			select $"G{index:00}").ToList());
		var allGroups = groupNames.Select((string groupName, int index) => new
		{
			Name = groupName,
			Id = normalizedGroupIds[index]
		}).ToList();
		List<(string, string)> vinculoGroups;
		if (vinculoGroupIdsFilter != null && vinculoGroupIdsFilter.Count > 0)
		{
			HashSet<string> selectedIds = new HashSet<string>(from groupId in vinculoGroupIdsFilter
				where !string.IsNullOrWhiteSpace(groupId)
				select groupId.Trim(), StringComparer.OrdinalIgnoreCase);
			vinculoGroups = (from @group in allGroups
				where selectedIds.Contains(@group.Id)
				select (Name: @group.Name, Id: @group.Id)).ToList();
			if (vinculoGroups.Count == 0)
			{
				vinculoGroups = allGroups.Select(group => (Name: group.Name, Id: group.Id)).ToList();
			}
		}
		else if (includeAllGroupsInVinculoQuestion)
		{
			vinculoGroups = allGroups.Select(group => (Name: group.Name, Id: group.Id)).ToList();
		}
		else
		{
			vinculoGroups = (from @group in allGroups
				where !string.Equals(NormalizeComparisonValue(@group.Name), "discentes ativos", StringComparison.Ordinal)
				select (Name: @group.Name, Id: @group.Id)).ToList();
			if (vinculoGroups.Count == 0)
			{
				vinculoGroups = allGroups.Select(group => (Name: group.Name, Id: group.Id)).ToList();
			}
		}
		string normalizedInstitution = (string.IsNullOrWhiteSpace(institutionName) ? "instituição não informada" : institutionName.Trim());
		string normalizedCourse = (string.IsNullOrWhiteSpace(courseName) ? "curso não informado" : courseName.Trim());
		string profileVinculoQuestion = $"Informe seu vínculo com o curso de {normalizedCourse} do(a) {normalizedInstitution}:";
		string profileVinculoMapping = string.Join("\n", vinculoGroups.Select<(string, string), string>(((string Name, string Id) group) => group.Id + " = " + group.Name));
		string profileVinculoTitle = EscapeJavaScriptSingleQuoted((useGroupCodesInVinculoQuestion && !string.IsNullOrWhiteSpace(profileVinculoMapping)) ? (profileVinculoQuestion + "\n\n" + profileVinculoMapping) : profileVinculoQuestion);
		string profileTempoTitle = EscapeJavaScriptSingleQuoted($"Tempo de atuação/vínculo com o curso de {normalizedCourse} do(a) {normalizedInstitution}:");
		string profileVinculoChoices = string.Join(", ", useGroupCodesInVinculoQuestion ? vinculoGroups.Select<(string, string), string>(((string Name, string Id) group) => "'" + EscapeJavaScriptSingleQuoted(group.Id) + "'") : vinculoGroups.Select<(string, string), string>(((string Name, string Id) group) => "'" + EscapeJavaScriptSingleQuoted(group.Name) + "'"));
		string tcleTitle = EscapeJavaScriptSingleQuoted("TERMO DE CONSENTIMENTO LIVRE E ESCLARECIDO (TCLE)");
		string tcleDescription = EscapeJavaScriptSingleQuoted("Declaro que li e compreendi as informações desta pesquisa. Minha participação é voluntária, posso interromper a qualquer momento e autorizo o uso das respostas para fins acadêmicos, de forma anônima.");
		string evaluationSectionTitle = EscapeJavaScriptSingleQuoted("AVALIAÇÃO DAS CAUSAS DE EVASÃO");
		string evaluationSectionDescription = EscapeJavaScriptSingleQuoted($"Instrução: com base na sua experiência e trajetória em relação ao curso de {normalizedCourse} do(a) {normalizedInstitution}, avalie em que medida cada fator abaixo contribui para a evasão de estudantes do curso, utilizando a escala Likert de 1 a 5.\n\n1 = Muito fraca\n2 = Fraca\n3 = Moderada\n4 = Forte\n5 = Muito forte\n\nMarque apenas uma opção por linha:");
		string causeChoices = string.Join(", ", causeRows.Select((string cause) => "'" + EscapeJavaScriptSingleQuoted(cause) + "'"));
		StringBuilder scriptBuilder = new StringBuilder();
		scriptBuilder.AppendLine("function criarFormulario() {");
		StringBuilder stringBuilder = scriptBuilder;
		StringBuilder stringBuilder2 = stringBuilder;
		StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(32, 1, stringBuilder);
		handler.AppendLiteral("  var form = FormApp.create('");
		handler.AppendFormatted(EscapeJavaScriptSingleQuoted(formTitle));
		handler.AppendLiteral("');");
		stringBuilder2.AppendLine(ref handler);
		if (!string.IsNullOrWhiteSpace(formDescription))
		{
			stringBuilder = scriptBuilder;
			StringBuilder stringBuilder3 = stringBuilder;
			handler = new StringBuilder.AppendInterpolatedStringHandler(26, 1, stringBuilder);
			handler.AppendLiteral("  form.setDescription('");
			handler.AppendFormatted(EscapeJavaScriptSingleQuoted(formDescription));
			handler.AppendLiteral("');");
			stringBuilder3.AppendLine(ref handler);
		}
		scriptBuilder.AppendLine("  var termoConsentimento = form.addMultipleChoiceItem();");
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder4 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(34, 1, stringBuilder);
		handler.AppendLiteral("  termoConsentimento.setTitle('");
		handler.AppendFormatted(tcleTitle);
		handler.AppendLiteral("');");
		stringBuilder4.AppendLine(ref handler);
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder5 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(37, 1, stringBuilder);
		handler.AppendLiteral("  termoConsentimento.setHelpText('");
		handler.AppendFormatted(tcleDescription);
		handler.AppendLiteral("');");
		stringBuilder5.AppendLine(ref handler);
		scriptBuilder.AppendLine("  termoConsentimento.setRequired(true);");
		scriptBuilder.AppendLine("  var secaoDadosIniciais = form.addPageBreakItem();");
		scriptBuilder.AppendLine("  secaoDadosIniciais.setTitle('PERFIL DO RESPONDENTE');");
		scriptBuilder.AppendLine("  termoConsentimento.setChoices([");
		scriptBuilder.AppendLine("    termoConsentimento.createChoice('Li e concordo em participar', secaoDadosIniciais),");
		scriptBuilder.AppendLine("    termoConsentimento.createChoice('Não concordo', FormApp.PageNavigationType.SUBMIT)");
		scriptBuilder.AppendLine("  ]);");
		scriptBuilder.AppendLine();
		scriptBuilder.AppendLine("  var perfilVinculo = form.addMultipleChoiceItem();");
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder6 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(29, 1, stringBuilder);
		handler.AppendLiteral("  perfilVinculo.setTitle('");
		handler.AppendFormatted(profileVinculoTitle);
		handler.AppendLiteral("');");
		stringBuilder6.AppendLine(ref handler);
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder7 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(36, 1, stringBuilder);
		handler.AppendLiteral("  perfilVinculo.setChoiceValues([");
		handler.AppendFormatted(profileVinculoChoices);
		handler.AppendLiteral("]);");
		stringBuilder7.AppendLine(ref handler);
		scriptBuilder.AppendLine("  perfilVinculo.setRequired(true);");
		scriptBuilder.AppendLine();
		scriptBuilder.AppendLine("  var perfilSexo = form.addMultipleChoiceItem();");
		scriptBuilder.AppendLine("  perfilSexo.setTitle('Sexo:');");
		scriptBuilder.AppendLine("  perfilSexo.setChoiceValues(['Feminino', 'Masculino', 'Prefiro não responder']);");
		scriptBuilder.AppendLine("  perfilSexo.setRequired(true);");
		scriptBuilder.AppendLine();
		scriptBuilder.AppendLine("  var perfilFaixaEtaria = form.addMultipleChoiceItem();");
		scriptBuilder.AppendLine("  perfilFaixaEtaria.setTitle('Faixa etária:');");
		scriptBuilder.AppendLine("  perfilFaixaEtaria.setChoiceValues(['Menos de 20 anos', '20 a 24 anos', '25 a 29 anos', '30 a 39 anos', '40 anos ou mais']);");
		scriptBuilder.AppendLine("  perfilFaixaEtaria.setRequired(true);");
		scriptBuilder.AppendLine();
		scriptBuilder.AppendLine("  var perfilTempo = form.addMultipleChoiceItem();");
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder8 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(27, 1, stringBuilder);
		handler.AppendLiteral("  perfilTempo.setTitle('");
		handler.AppendFormatted(profileTempoTitle);
		handler.AppendLiteral("');");
		stringBuilder8.AppendLine(ref handler);
		scriptBuilder.AppendLine("  perfilTempo.setChoiceValues(['Menos de 2 anos', 'De 2 a 5 anos', 'De 6 a 10 anos', 'Mais de 10 anos']);");
		scriptBuilder.AppendLine("  perfilTempo.setRequired(true);");
		scriptBuilder.AppendLine();
		if (includeProfilePrincipalAtuacaoQuestion)
		{
			string profilePrincipalAtuacaoTitle = EscapeJavaScriptSingleQuoted($"Principal perfil de atuação no curso de {normalizedCourse} do(a) {normalizedInstitution} (pode marcar mais de uma opção):");
			scriptBuilder.AppendLine("  var perfilPrincipalAtuacao = form.addCheckboxItem();");
			stringBuilder = scriptBuilder;
			StringBuilder stringBuilder9 = stringBuilder;
			handler = new StringBuilder.AppendInterpolatedStringHandler(38, 1, stringBuilder);
			handler.AppendLiteral("  perfilPrincipalAtuacao.setTitle('");
			handler.AppendFormatted(profilePrincipalAtuacaoTitle);
			handler.AppendLiteral("');");
			stringBuilder9.AppendLine(ref handler);
			scriptBuilder.AppendLine("  perfilPrincipalAtuacao.setChoiceValues([");
			scriptBuilder.AppendLine("    'Disciplinas/atividades de base ou formação inicial (por exemplo, conteúdos introdutórios e fundamentos)',");
			scriptBuilder.AppendLine("    'Disciplinas/atividades de formação específica do curso (por exemplo, componentes centrais da área)',");
			scriptBuilder.AppendLine("    'Disciplinas/atividades de prática profissional, integração teoria-prática e/ou metodologias aplicadas',");
			scriptBuilder.AppendLine("    'Disciplinas/atividades das etapas finais do curso (por exemplo, estágio, TCC, projeto integrador, internato)',");
			scriptBuilder.AppendLine("    'Ações de apoio estudantil, monitoria, tutoria ou projetos de acolhimento'");
			scriptBuilder.AppendLine("  ]);");
			scriptBuilder.AppendLine("  perfilPrincipalAtuacao.setRequired(true);");
			scriptBuilder.AppendLine();
		}
		scriptBuilder.AppendLine("  var secaoQuestionario = form.addPageBreakItem();");
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder10 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(33, 1, stringBuilder);
		handler.AppendLiteral("  secaoQuestionario.setTitle('");
		handler.AppendFormatted(evaluationSectionTitle);
		handler.AppendLiteral("');");
		stringBuilder10.AppendLine(ref handler);
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder11 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(36, 1, stringBuilder);
		handler.AppendLiteral("  secaoQuestionario.setHelpText('");
		handler.AppendFormatted(evaluationSectionDescription);
		handler.AppendLiteral("');");
		stringBuilder11.AppendLine(ref handler);
		scriptBuilder.AppendLine();
		scriptBuilder.AppendLine("  var causasItem = form.addGridItem();");
		scriptBuilder.AppendLine("  causasItem.setTitle('Causa da evasão:');");
		stringBuilder = scriptBuilder;
		StringBuilder stringBuilder12 = stringBuilder;
		handler = new StringBuilder.AppendInterpolatedStringHandler(25, 1, stringBuilder);
		handler.AppendLiteral("  causasItem.setRows([");
		handler.AppendFormatted(causeChoices);
		handler.AppendLiteral("]);");
		stringBuilder12.AppendLine(ref handler);
		scriptBuilder.AppendLine("  causasItem.setColumns(['1', '2', '3', '4', '5']);");
		scriptBuilder.AppendLine("  causasItem.setRequired(true);");
		scriptBuilder.AppendLine();
		if (includePairQuestions)
		{
			scriptBuilder.AppendLine("  var secaoComparacoesAhp = form.addPageBreakItem();");
			scriptBuilder.AppendLine("  secaoComparacoesAhp.setTitle('COMPARAÇÃO DE IMPORTÂNCIA DOS GRUPOS');");
			scriptBuilder.AppendLine("  secaoComparacoesAhp.setHelpText('Nesta seção, compare qual grupo deve ter mais peso na definição das prioridades de ação. Use a escala de 1 a 9.\\n\\n1 = Igual\\n3 = Moderada\\n5 = Forte\\n7 = Muito forte\\n9 = Extrema\\n\\n2, 4, 6, 8 = valores intermediários.');");
			scriptBuilder.AppendLine();
			int questionNumber = 2;
			for (int i = 0; i < groupNames.Count; i++)
			{
				for (int j = i + 1; j < groupNames.Count; j++)
				{
					string groupNameA = groupNames[i];
					string groupNameB = groupNames[j];
					string groupAOption = EscapeJavaScriptSingleQuoted(groupNameA);
					string groupBOption = EscapeJavaScriptSingleQuoted(groupNameB);
					string titleA = EscapeJavaScriptSingleQuoted(groupNameA + " ou " + groupNameB + ": qual grupo deve ter mais peso na decisão das prioridades?");
					string titleB = EscapeJavaScriptSingleQuoted($"Na comparação {groupNameA} ou {groupNameB} acima, qual é a força dessa diferença? Se você marcou \"Igual\", assinale 1.");
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder13 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(46, 1, stringBuilder);
					handler.AppendLiteral("  var pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(" = form.addMultipleChoiceItem();");
					stringBuilder13.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder14 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(24, 2, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setTitle('");
					handler.AppendFormatted(titleA);
					handler.AppendLiteral("');");
					stringBuilder14.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder15 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(46, 3, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setChoiceValues(['");
					handler.AppendFormatted(groupAOption);
					handler.AppendLiteral("', '");
					handler.AppendFormatted(groupBOption);
					handler.AppendLiteral("', 'Igual']);");
					stringBuilder15.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder16 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(29, 1, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setRequired(true);");
					stringBuilder16.AppendLine(ref handler);
					scriptBuilder.AppendLine();
					questionNumber++;
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder17 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(37, 1, stringBuilder);
					handler.AppendLiteral("  var pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(" = form.addScaleItem();");
					stringBuilder17.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder18 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(24, 2, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setTitle('");
					handler.AppendFormatted(titleB);
					handler.AppendLiteral("');");
					stringBuilder18.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder19 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(27, 1, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setBounds(1, 9);");
					stringBuilder19.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder20 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(41, 1, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setLabels('Igual', 'Extrema');");
					stringBuilder20.AppendLine(ref handler);
					stringBuilder = scriptBuilder;
					StringBuilder stringBuilder21 = stringBuilder;
					handler = new StringBuilder.AppendInterpolatedStringHandler(29, 1, stringBuilder);
					handler.AppendLiteral("  pergunta");
					handler.AppendFormatted(questionNumber);
					handler.AppendLiteral(".setRequired(true);");
					stringBuilder21.AppendLine(ref handler);
					scriptBuilder.AppendLine();
					questionNumber++;
				}
			}
		}
		scriptBuilder.AppendLine("}");
		return scriptBuilder.ToString();
	}

	private static string BuildQ1FormDescription(string institution, string course)
	{
		string normalizedInstitution = (string.IsNullOrWhiteSpace(institution) ? "instituição não informada" : institution.Trim());
		string normalizedCourse = (string.IsNullOrWhiteSpace(course) ? "curso não informado" : course.Trim());
		return "Prezados(as),\n\n" + $"Este questionário ajuda a validar as possíveis causas de evasão no curso de {normalizedCourse} do(a) {normalizedInstitution}. " + "Ele também coleta comparações entre grupos participantes para definir os pesos usados na priorização. A participação é voluntária e anônima. O tempo estimado de preenchimento é de 10 a 15 minutos.";
	}

	private async Task ShowQ1ExportNextStepsDialogAsync(string saveLocation, string scriptContent)
	{
		await ShowFormsExportNextStepsDialogAsync(saveLocation, "Importar respostas de aprovação", includesPairQuestions: true, scriptContent);
	}

	private async Task ShowQ2ExportNextStepsDialogAsync(string saveLocation, string scriptContent)
	{
		await ShowFormsExportNextStepsDialogAsync(saveLocation, "Importar respostas finais", includesPairQuestions: false, scriptContent);
	}

	private async Task ShowFormsExportNextStepsDialogAsync(string saveLocation, string importButtonLabel, bool includesPairQuestions, string scriptContent)
	{
		string savedFileName = Path.GetFileName(saveLocation);
		if (string.IsNullOrWhiteSpace(savedFileName))
		{
			savedFileName = "arquivo salvo";
		}
		string instructions = "1. Acesse https://script.google.com/ e crie um projeto.\n2. Abra Código.gs e apague o conteúdo atual.\n3. Cole todo o conteúdo do arquivo " + savedFileName + ".\n4. Salve e execute a função criarFormulario.\n5. Autorize as permissões solicitadas pelo Google.\n6. Abra o Google Forms criado e compartilhe o link com os participantes.\n7. Depois da coleta, baixe as respostas em CSV no Google Forms.\n8. No SIGEV, use o botão \"" + importButtonLabel + "\". Não é preciso renomear o CSV baixado.";
		StackPanel contentPanel = new StackPanel
		{
			Spacing = 8.0,
			MinWidth = 540.0
		};
		contentPanel.Children.Add(new TextBlock
		{
			Text = "Arquivo salvo em:\n" + saveLocation,
			TextWrapping = TextWrapping.WrapWholeWords
		});
		contentPanel.Children.Add(new TextBlock
		{
			Text = "Passo a passo:"
		});
		contentPanel.Children.Add(new TextBlock
		{
			Text = instructions,
			TextWrapping = TextWrapping.WrapWholeWords
		});
		TextBlock actionStatusTextBlock = new TextBlock
		{
			Text = string.Empty,
			TextWrapping = TextWrapping.WrapWholeWords
		};
		contentPanel.Children.Add(actionStatusTextBlock);
		ContentDialog contentDialog = new ContentDialog();
		contentDialog.Title = "Questionário pronto para Google Forms";
		contentDialog.Content = contentPanel;
		contentDialog.PrimaryButtonText = "Abrir Google Apps Script";
		contentDialog.SecondaryButtonText = "Copiar script";
		contentDialog.CloseButtonText = "Fechar";
		contentDialog.DefaultButton = ContentDialogButton.Close;
		contentDialog.XamlRoot = base.XamlRoot;
		contentDialog.PrimaryButtonClick += async delegate(ContentDialog _, ContentDialogButtonClickEventArgs args)
		{
			args.Cancel = true;
			ContentDialogButtonClickDeferral deferral = args.GetDeferral();
			try
			{
				bool launched = await Launcher.LaunchUriAsync(new Uri("https://script.google.com/"));
				actionStatusTextBlock.Text = (launched ? "Google Apps Script aberto. O diálogo permanece aberto." : "Não foi possível abrir o Google Apps Script automaticamente.");
			}
			finally
			{
				deferral.Complete();
			}
		};
		contentDialog.SecondaryButtonClick += delegate(ContentDialog _, ContentDialogButtonClickEventArgs args)
		{
			args.Cancel = true;
			DataPackage dataPackage = new DataPackage();
			dataPackage.SetText(scriptContent);
			Clipboard.SetContent(dataPackage);
			Clipboard.Flush();
			actionStatusTextBlock.Text = "Script copiado para a área de transferência. O diálogo permanece aberto.";
		};
		await contentDialog.ShowAsync();
	}

	private async void OnImportQ2CsvClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		string sourceCsvPath = await PickCsvFilePathAsync();
		if (string.IsNullOrWhiteSpace(sourceCsvPath))
		{
			return;
		}
		string destinationCsvPath = Path.Combine(currentProject.FolderPath, FinalAnswersCsvFileName);
		try
		{
			Directory.CreateDirectory(currentProject.FolderPath);
			File.Copy(sourceCsvPath, destinationCsvPath, overwrite: true);
			LoadQ2TableFromFile(destinationCsvPath);
			MarkProjectAsModified();
			await ShowMessageAsync("Importação concluída", "Respostas finais importadas para:\n" + destinationCsvPath);
		}
		catch (Exception ex)
		{
			await ShowMessageAsync("Erro ao importar", "Não foi possível importar as respostas finais. Verifique se o CSV veio do questionário final.\n\n" + ex.Message);
		}
	}

	private async void OnClearQ2TableClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject != null)
		{
			string contentText = ((q2Rows.Count > 0) ? $"Deseja realmente limpar o ranking final ({q2Rows.Count} linhas)?" : "Deseja realmente limpar o ranking final?");
			if (await new ContentDialog
			{
				Title = "Confirmar limpeza",
				Content = contentText,
				PrimaryButtonText = "Limpar",
				CloseButtonText = "Cancelar",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = base.XamlRoot
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				q2Rows.Clear();
				q2Alternatives.Clear();
				q2ResponseCount = 0;
				TopsisStatusTextBlock.Text = "Ranking final limpo.";
				RefreshReportsSection();
			}
		}
	}

	private void OnRefreshReportsClicked(object sender, RoutedEventArgs e)
	{
		RefreshReportsSection();
	}

	private async void OnCopyReportsSummaryClicked(object sender, RoutedEventArgs e)
	{
		string summary = BuildReportsSummaryText();
		DataPackage dataPackage = new DataPackage();
		dataPackage.SetText(summary);
		Clipboard.SetContent(dataPackage);
		Clipboard.Flush();
		await ShowMessageAsync("Copiado", "Resumo copiado para a área de transferência.");
	}

	private async void OnExportReportsCsvClicked(object sender, RoutedEventArgs e)
	{
		if (currentProject == null)
		{
			return;
		}
		string csvSavePath = await PickCsvSavePathAsync(BuildSuggestedFileName("relatorio_sigev"));
		if (string.IsNullOrWhiteSpace(csvSavePath))
		{
			return;
		}
		try
		{
			File.WriteAllText(csvSavePath, BuildReportsCsvContent(), Encoding.UTF8);
			MarkProjectAsModified();
			RefreshReportsSection();
			ReportsTextBlock.Text = "Relatório exportado em: " + csvSavePath + "\n" + ReportsTextBlock.Text;
		}
		catch (Exception ex)
		{
			await ShowMessageAsync("Erro ao exportar", "Não foi possível exportar o resumo.\n\n" + ex.Message);
		}
	}

	private List<string> BuildExpectedCauseLabels()
	{
		return (from item in causes
			where !string.IsNullOrWhiteSpace(item.Name)
			select item.ItemId + ". " + item.Name.Trim()).ToList();
	}

	private List<string> BuildApprovedCauseLabelsForQ2()
	{
		return (from row in q1Rows
			where string.Equals(row.Status, "Aprovada", StringComparison.OrdinalIgnoreCase)
			select row.Number + ". " + row.Cause).ToList();
	}

	private static string EscapeJavaScriptSingleQuoted(string value)
	{
		return value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("'", "\\'", StringComparison.Ordinal).Replace("\r\n", "\\n", StringComparison.Ordinal)
			.Replace("\r", "\\n", StringComparison.Ordinal)
			.Replace("\n", "\\n", StringComparison.Ordinal);
	}

	private void LoadCauses()
	{
		causes.Clear();
		if (currentProject == null)
		{
			UpdateCauseButtons();
			UpdateCausesStatus();
			RefreshReportsSection();
			return;
		}
		foreach (string causeName in ProjectCausesRepository.LoadCauses(currentProject.FolderPath))
		{
			causes.Add(new NamedListItem(causeName, "C"));
		}
		RenumberItems(causes);
		UpdateCauseButtons();
		UpdateCausesStatus();
		RefreshReportsSection();
	}

	private void SaveCauses()
	{
		if (currentProject == null)
		{
			return;
		}
		try
		{
			ProjectCausesRepository.SaveCauses(currentProject.FolderPath, causes.Select((NamedListItem item) => item.Name));
			MarkProjectAsModified();
		}
		catch
		{
			UpdateCausesStatus("Erro ao salvar causas.");
		}
	}

	private void UpdateCauseButtons()
	{
		bool hasSelection = CausesListView.SelectedItem is NamedListItem;
		RenameCauseButton.IsEnabled = hasSelection;
		DeleteCauseButton.IsEnabled = hasSelection;
	}

	private void UpdateGroupsStatus(string? actionMessage = null)
	{
		string countMessage = groups.Count switch
		{
			0 => "Nenhum grupo participante cadastrado.", 
			1 => "1 grupo participante cadastrado.", 
			_ => $"{groups.Count} grupos participantes cadastrados.", 
		};
		GroupsStatusTextBlock.Text = (string.IsNullOrWhiteSpace(actionMessage) ? countMessage : (actionMessage + " " + countMessage));
	}

	private void RebuildAhpTableRows(string? actionMessage = null)
	{
		EnsureAhpUiBindings();
		ahpRows.Clear();
		bool hasComputedData = HasAhpComputedDataForCurrentGroups();
		foreach (NamedListItem group in groups)
		{
			double weight = 0.0;
			string weightText = ((hasComputedData && ahpWeightsByGroupId.TryGetValue(group.ItemId, out weight)) ? ((weight * 100.0).ToString("F1", CultureInfo.CurrentCulture) + "%") : "-");
			ahpRows.Add(new AhpTableRow(group.ItemId, group.Name, weightText));
		}
		UpdateAhpGlobalCrSummary(hasComputedData);
		string defaultMessage = groups.Count switch
		{
			0 => "Nenhum grupo participante disponível para calcular pesos.", 
			1 => "1 grupo participante disponível para calcular pesos.", 
			_ => $"{groups.Count} grupos participantes disponíveis para calcular pesos.", 
		};
		string countMessage = ((hasComputedData && ahpCrPercent.HasValue) ? $"{groups.Count} grupos participantes ponderados com {ahpResponseCount} respostas. Consistência = {ahpCrPercent.Value.ToString("F1", CultureInfo.CurrentCulture)}%." : defaultMessage);
		if ((object)TeamAhpTextBlock != null)
		{
			TeamAhpTextBlock.Text = (string.IsNullOrWhiteSpace(actionMessage) ? countMessage : (actionMessage + " " + countMessage));
		}
		RebuildQ2TableRows();
	}

	private void UpdateAhpGlobalCrSummary(bool hasComputedData)
	{
		if ((object)AhpGlobalCrTextBlock == null || (object)AhpCrStatusBadgeBorder == null || (object)AhpCrStatusBadgeTextBlock == null)
		{
			return;
		}
		if (!hasComputedData || !ahpCrPercent.HasValue)
		{
			AhpGlobalCrTextBlock.Text = "-";
			AhpCrStatusBadgeTextBlock.Text = "Pendente";
			AhpCrStatusBadgeBorder.Background = GetThemeBrushOrFallback("SubtleFillColorSecondaryBrush", new SolidColorBrush(ColorHelper.FromArgb(byte.MaxValue, 242, 242, 242)));
			AhpCrStatusBadgeBorder.BorderBrush = GetThemeBrushOrFallback("CardStrokeColorDefaultBrush", new SolidColorBrush(ColorHelper.FromArgb(byte.MaxValue, 200, 200, 200)));
			AhpCrStatusBadgeTextBlock.Foreground = GetThemeBrushOrFallback("TextFillColorPrimaryBrush", new SolidColorBrush(ColorHelper.FromArgb(byte.MaxValue, 40, 40, 40)));
			return;
		}
		double cr = ahpCrPercent.Value;
		AhpGlobalCrTextBlock.Text = cr.ToString("F1", CultureInfo.CurrentCulture) + "%";
		double consistencyLimit = (((object)AhpConsistencyNumberBox != null && !double.IsNaN(AhpConsistencyNumberBox.Value)) ? AhpConsistencyNumberBox.Value : 0.0);
		if (cr <= consistencyLimit)
		{
			SetAhpCrBadge("Comparações consistentes (<= " + consistencyLimit.ToString("F1", CultureInfo.CurrentCulture) + "%)", ColorHelper.FromArgb(byte.MaxValue, 223, 246, 221), ColorHelper.FromArgb(byte.MaxValue, 45, 125, 47), ColorHelper.FromArgb(byte.MaxValue, 27, 94, 32));
		}
		else
		{
			SetAhpCrBadge("Comparações inconsistentes (> " + consistencyLimit.ToString("F1", CultureInfo.CurrentCulture) + "%)", ColorHelper.FromArgb(byte.MaxValue, 253, 231, 233), ColorHelper.FromArgb(byte.MaxValue, 197, 15, 31), ColorHelper.FromArgb(byte.MaxValue, 138, 17, 29));
		}
	}

	private void SetAhpCrBadge(string text, Color background, Color border, Color foreground)
	{
		if ((object)AhpCrStatusBadgeBorder != null && (object)AhpCrStatusBadgeTextBlock != null)
		{
			AhpCrStatusBadgeTextBlock.Text = text;
			AhpCrStatusBadgeBorder.Background = new SolidColorBrush(background);
			AhpCrStatusBadgeBorder.BorderBrush = new SolidColorBrush(border);
			AhpCrStatusBadgeTextBlock.Foreground = new SolidColorBrush(foreground);
		}
	}

	private static Brush GetThemeBrushOrFallback(string key, Brush fallback)
	{
		if (Application.Current.Resources.TryGetValue(key, out var resource) && resource is Brush brush)
		{
			return brush;
		}
		return fallback;
	}

	private bool HasAhpComputedDataForCurrentGroups()
	{
		if (!ahpCrPercent.HasValue || groups.Count == 0 || ahpWeightsByGroupId.Count != groups.Count)
		{
			return false;
		}
		return groups.All((NamedListItem group) => ahpWeightsByGroupId.ContainsKey(group.ItemId));
	}

	private void UpdateCausesStatus(string? actionMessage = null)
	{
		string countMessage = causes.Count switch
		{
			0 => "Nenhuma causa possível cadastrada.", 
			1 => "1 causa possível cadastrada.", 
			_ => $"{causes.Count} causas possíveis cadastradas.", 
		};
		CausesStatusTextBlock.Text = (string.IsNullOrWhiteSpace(actionMessage) ? countMessage : (actionMessage + " " + countMessage));
	}

	private static void RenumberItems(IList<NamedListItem> items)
	{
		for (int i = 0; i < items.Count; i++)
		{
			items[i].Number = i + 1;
		}
	}

	private async Task ShowMessageAsync(string title, string message)
	{
		await new ContentDialog
		{
			Title = title,
			Content = message,
			CloseButtonText = "Fechar",
			DefaultButton = ContentDialogButton.Close,
			XamlRoot = base.XamlRoot
		}.ShowAsync();
	}

	private void OnBackClicked(object sender, RoutedEventArgs e)
	{
		Frame frame = base.Frame;
		if ((object)frame != null && frame.CanGoBack)
		{
			base.Frame.GoBack();
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Views/ProjectDetailsPage.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Application);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 2:
			PageRootGrid = target.As<Grid>();
			PageRootGrid.Tapped += OnPageBackgroundTapped;
			break;
		case 3:
			ReportsTextBlock = target.As<TextBlock>();
			break;
		case 4:
			ReportsTopTopsisStatusTextBlock = target.As<TextBlock>();
			break;
		case 5:
			ReportsTopTopsisListView = target.As<ListView>();
			break;
		case 11:
			ReportsTopCauseValueTextBlock = target.As<TextBlock>();
			break;
		case 12:
			ReportsCrGlobalValueTextBlock = target.As<TextBlock>();
			break;
		case 13:
			ReportsApprovedCausesValueTextBlock = target.As<TextBlock>();
			break;
		case 14:
			ReportsRespondentsValueTextBlock = target.As<TextBlock>();
			break;
		case 15:
			SetButtonText(target, "Atualizar relatório").Click += OnRefreshReportsClicked;
			break;
		case 16:
			SetButtonText(target, "Exportar relatório em CSV").Click += OnExportReportsCsvClicked;
			break;
		case 17:
			SetButtonText(target, "Copiar resumo do relatório").Click += OnCopyReportsSummaryClicked;
			break;
		case 18:
			SetButtonText(target, "Importar respostas finais").Click += OnImportQ2CsvClicked;
			break;
		case 19:
			SetButtonText(target, "Limpar ranking").Click += OnClearQ2TableClicked;
			break;
		case 20:
			TopsisStatusTextBlock = target.As<TextBlock>();
			break;
		case 21:
			Q2TableListView = target.As<ListView>();
			break;
		case 27:
			TeamAhpTextBlock = target.As<TextBlock>();
			break;
		case 28:
			AhpTableListView = target.As<ListView>();
			break;
		case 34:
			SetButtonText(target, "Gerar questionário final").Click += OnExportQ2TxtClicked;
			break;
		case 35:
			AhpGlobalCrTextBlock = target.As<TextBlock>();
			break;
		case 36:
			AhpCrStatusBadgeBorder = target.As<Border>();
			break;
		case 37:
			AhpCrStatusBadgeTextBlock = target.As<TextBlock>();
			break;
		case 38:
			AhpConsistencyNumberBox = target.As<NumberBox>();
			AhpConsistencyNumberBox.ValueChanged += OnAhpConsistencyValueChanged;
			AhpConsistencyNumberBox.LostFocus += OnAhpConsistencyLostFocus;
			break;
		case 39:
			TeamAssignmentsTextBlock = target.As<TextBlock>();
			break;
		case 40:
			Q1TableListView = target.As<ListView>();
			break;
		case 53:
			SetButtonText(target, "Limpar aprovação").Click += OnClearQ1TableClicked;
			break;
		case 54:
			Scale5CheckBox = target.As<CheckBox>();
			Scale5CheckBox.Checked += OnScaleSelectionChanged;
			Scale5CheckBox.Unchecked += OnScaleSelectionChanged;
			break;
		case 55:
			Scale4CheckBox = target.As<CheckBox>();
			Scale4CheckBox.Checked += OnScaleSelectionChanged;
			Scale4CheckBox.Unchecked += OnScaleSelectionChanged;
			break;
		case 56:
			Scale3CheckBox = target.As<CheckBox>();
			Scale3CheckBox.Checked += OnScaleSelectionChanged;
			Scale3CheckBox.Unchecked += OnScaleSelectionChanged;
			break;
		case 57:
			Scale2CheckBox = target.As<CheckBox>();
			Scale2CheckBox.Checked += OnScaleSelectionChanged;
			Scale2CheckBox.Unchecked += OnScaleSelectionChanged;
			break;
		case 58:
			Scale1CheckBox = target.As<CheckBox>();
			Scale1CheckBox.Checked += OnScaleSelectionChanged;
			Scale1CheckBox.Unchecked += OnScaleSelectionChanged;
			break;
		case 59:
			ValidationProportionNumberBox = target.As<NumberBox>();
			ValidationProportionNumberBox.ValueChanged += OnValidationProportionValueChanged;
			ValidationProportionNumberBox.LostFocus += OnValidationProportionLostFocus;
			break;
		case 60:
			ValidationMedianNumberBox = target.As<NumberBox>();
			ValidationMedianNumberBox.ValueChanged += OnValidationMedianValueChanged;
			ValidationMedianNumberBox.LostFocus += OnValidationMedianLostFocus;
			break;
		case 61:
			SetButtonText(target, "Importar respostas de aprovação").Click += OnImportQ1CsvClicked;
			break;
		case 62:
			CausesStatusTextBlock = target.As<TextBlock>();
			break;
		case 63:
			CausesListView = target.As<ListView>();
			CausesListView.SelectionChanged += OnCausesSelectionChanged;
			break;
		case 68:
			SetButtonText(target, "Gerar questionário de validação").Click += OnExportQ1TxtClicked;
			break;
		case 69:
			Q1VinculoGroupsItemsControl = target.As<ItemsControl>();
			break;
		case 74:
			SetButtonText(target, "Importar lista de causas").Click += OnImportCausesCsvClicked;
			break;
		case 75:
			SetButtonText(target, "Baixar modelo de causas").Click += OnDownloadCausesTemplateClicked;
			break;
		case 76:
			SetButtonText(target, "Limpar causas possíveis").Click += OnClearCausesClicked;
			break;
		case 77:
			DeleteCauseButton = target.As<Button>();
			DeleteCauseButton.Click += OnDeleteCauseClicked;
			break;
		case 78:
			RenameCauseButton = SetButtonText(target, "Editar nome");
			RenameCauseButton.Click += OnRenameCauseClicked;
			break;
		case 79:
			SetButtonText(target, "Adicionar causa possível").Click += OnAddCauseClicked;
			break;
		case 80:
			GroupsStatusTextBlock = target.As<TextBlock>();
			break;
		case 81:
			GroupsListView = target.As<ListView>();
			GroupsListView.SelectionChanged += OnGroupsSelectionChanged;
			break;
		case 86:
			SetButtonText(target, "Adicionar participante").Click += OnAddGroupClicked;
			break;
		case 87:
			RenameGroupButton = SetButtonText(target, "Editar nome");
			RenameGroupButton.Click += OnRenameGroupClicked;
			break;
		case 88:
			DeleteGroupButton = target.As<Button>();
			DeleteGroupButton.Click += OnDeleteGroupClicked;
			break;
		case 89:
			SetButtonText(target, "Limpar participantes").Click += OnClearGroupsClicked;
			break;
		case 90:
			ProjectNameTextBlock = target.As<TextBlock>();
			break;
		case 91:
			ProjectFolderTextBlock = target.As<TextBlock>();
			break;
		case 92:
			ProjectCreatedTextBlock = target.As<TextBlock>();
			break;
		case 93:
			ProjectModifiedTextBlock = target.As<TextBlock>();
			break;
		case 94:
			target.As<Button>().Click += OnBackClicked;
			break;
		case 95:
			PageTitleTextBlock = target.As<TextBlock>();
			break;
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2602")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		IComponentConnector returnValue = null;
		switch (connectionId)
		{
		case 7:
		{
			Grid element89 = (Grid)target;
			ProjectDetailsPage_obj7_Bindings bindings7 = new ProjectDetailsPage_obj7_Bindings();
			returnValue = bindings7;
			bindings7.SetDataRoot(element89.DataContext);
			element89.DataContextChanged += bindings7.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element89, bindings7);
			XamlBindingHelper.SetDataTemplateComponent(element89, bindings7);
			break;
		}
		case 23:
		{
			Grid element88 = (Grid)target;
			ProjectDetailsPage_obj23_Bindings bindings6 = new ProjectDetailsPage_obj23_Bindings();
			returnValue = bindings6;
			bindings6.SetDataRoot(element88.DataContext);
			element88.DataContextChanged += bindings6.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element88, bindings6);
			XamlBindingHelper.SetDataTemplateComponent(element88, bindings6);
			break;
		}
		case 30:
		{
			Grid element87 = (Grid)target;
			ProjectDetailsPage_obj30_Bindings bindings5 = new ProjectDetailsPage_obj30_Bindings();
			returnValue = bindings5;
			bindings5.SetDataRoot(element87.DataContext);
			element87.DataContextChanged += bindings5.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element87, bindings5);
			XamlBindingHelper.SetDataTemplateComponent(element87, bindings5);
			break;
		}
		case 42:
		{
			Grid element86 = (Grid)target;
			ProjectDetailsPage_obj42_Bindings bindings4 = new ProjectDetailsPage_obj42_Bindings();
			returnValue = bindings4;
			bindings4.SetDataRoot(element86.DataContext);
			element86.DataContextChanged += bindings4.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element86, bindings4);
			XamlBindingHelper.SetDataTemplateComponent(element86, bindings4);
			break;
		}
		case 65:
		{
			Grid element85 = (Grid)target;
			ProjectDetailsPage_obj65_Bindings bindings3 = new ProjectDetailsPage_obj65_Bindings();
			returnValue = bindings3;
			bindings3.SetDataRoot(element85.DataContext);
			element85.DataContextChanged += bindings3.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element85, bindings3);
			XamlBindingHelper.SetDataTemplateComponent(element85, bindings3);
			break;
		}
		case 71:
		{
			StackPanel element84 = (StackPanel)target;
			ProjectDetailsPage_obj71_Bindings bindings2 = new ProjectDetailsPage_obj71_Bindings();
			returnValue = bindings2;
			bindings2.SetDataRoot(element84.DataContext);
			element84.DataContextChanged += bindings2.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element84, bindings2);
			XamlBindingHelper.SetDataTemplateComponent(element84, bindings2);
			break;
		}
		case 83:
		{
			Grid element83 = (Grid)target;
			ProjectDetailsPage_obj83_Bindings bindings = new ProjectDetailsPage_obj83_Bindings();
			returnValue = bindings;
			bindings.SetDataRoot(element83.DataContext);
			element83.DataContextChanged += bindings.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(element83, bindings);
			XamlBindingHelper.SetDataTemplateComponent(element83, bindings);
			break;
		}
		}
		return returnValue;
	}
}
